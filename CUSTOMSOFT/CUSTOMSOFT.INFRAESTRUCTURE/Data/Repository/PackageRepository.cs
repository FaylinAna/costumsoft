using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CUSTOMSOFT.INFRAESTRUCTURE.Data.Repository
{
    public class PackageRepository : IPackageRepository
    {
        private readonly ConnectionSettings _connectionSettings;

        public PackageRepository(ConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
        }

        public async Task<Package> AddPackage(PackageDTO package)
        {
            try
            {
                using var connection = _connectionSettings.OpenSQLConnectionAsync();

                var parameters = GetParameterList(package);
                parameters.Add("p_tracking_number", package.TrackingNumber, DbType.String, ParameterDirection.Input);
                var res = await connection.QueryFirstAsync<int>("SELECT add_package(@p_tracking_number, @p_customer_name, @p_delivery_address, @p_weight)", parameters);
                return new Package { IdPackage = res, TrackingNumber = package.TrackingNumber };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Package>> GetAllPackages()
        {
            try
            {
                using var connection = _connectionSettings.OpenSQLConnectionAsync();
                var packagesDictionary = new Dictionary<int, Package>();
                var response = (await connection.QueryAsync<Package, PackageState, Package>(
                    "SELECT * FROM  public.get_all_packages()",
                    (pkg, state) =>
                    {
                        if (!packagesDictionary.TryGetValue(pkg.IdPackage, out var package))
                        {
                            package = pkg;
                            package.States = new List<PackageState>();
                            packagesDictionary.Add(package.IdPackage, package);
                        }

                        if (state != null)
                        {
                            package.States.Add(state);
                        }

                        return pkg;
                    },
                    null,
                    splitOn: "IdPackageStates"

                )).Distinct().ToList();
                return packagesDictionary.Values.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Package> GetByTrackingNumberAsync(string trackingNumber)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_trackingNumber", trackingNumber, DbType.String, ParameterDirection.Input);

                using var connection = _connectionSettings.OpenSQLConnectionAsync();
                var packagesDictionary = new Dictionary<int, Package>();
                var response = (await connection.QueryAsync<Package, PackageState, Package>(
                    "SELECT * FROM  public.get_package_by_tracking_number(@p_trackingNumber)",
                    (pkg, state) =>
                    {
                        if (!packagesDictionary.TryGetValue(pkg.IdPackage, out var package))
                        {
                            package = pkg;
                            package.States = new List<PackageState>();
                            packagesDictionary.Add(package.IdPackage, package);
                        }

                        if (state != null)
                        {
                            package.States.Add(state);
                        }

                        return pkg;
                    },
                    parameters,
                    splitOn: "idPackageStates"

                )).Distinct().ToList();
                return packagesDictionary.Values?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Package> UpdatePackage(PackageDTO package)
        {
            try
            {
                var parameters = GetParameterList(package);
                parameters.Add("p_package_id", package.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_state_description", package.StateDescription, DbType.String, ParameterDirection.Input);

                using var connection = _connectionSettings.OpenSQLConnectionAsync();

                var res = await connection.QueryFirstAsync("SELECT update_package(@p_package_id, @p_customer_name, @p_delivery_address, @p_weight,@p_state_description)", parameters);
                return new Package
                {
                    IdPackage = package.Id,
                    TrackingNumber = package.TrackingNumber,
                    CustomerName = package.CustomerName,
                    DeliveryAddress = package.DeliveryAddress,
                    Weight = package.Weight,
                    
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static DynamicParameters GetParameterList(PackageDTO package)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_customer_name", package.CustomerName, DbType.String, ParameterDirection.Input);
            parameters.Add("p_delivery_address", package.DeliveryAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("p_weight", package.Weight, DbType.Decimal, ParameterDirection.Input);

            return parameters;
        }

    }
}
