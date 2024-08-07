PGDMP  '                    |            ProyectosVue    16.2    16.2 1               0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16567    ProyectosVue    DATABASE     �   CREATE DATABASE "ProyectosVue" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Spanish_Spain.1252';
    DROP DATABASE "ProyectosVue";
                postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
                pg_database_owner    false                       0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                   pg_database_owner    false    4            �            1255    16777 J   actualizar_proyecto(integer, character varying, text, date, date, integer) 	   PROCEDURE     %  CREATE PROCEDURE public.actualizar_proyecto(IN id_proyecto integer, IN nombre_proyecto character varying, IN descripcion_proyecto text, IN fecha_inicio_proyecto date, IN fecha_fin_proyecto date, IN id_usuario_lider_u integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    UPDATE proyecto
    SET nombre = nombre_proyecto,
        descripcion = descripcion_proyecto,
        fecha_inicio = fecha_inicio_proyecto,
        fecha_fin = fecha_fin_proyecto,
        id_usuario_lider = id_usuario_lider_u
    WHERE id_proyecto = id_proyecto;
END;
$$;
 �   DROP PROCEDURE public.actualizar_proyecto(IN id_proyecto integer, IN nombre_proyecto character varying, IN descripcion_proyecto text, IN fecha_inicio_proyecto date, IN fecha_fin_proyecto date, IN id_usuario_lider_u integer);
       public          postgres    false    4            �            1255    16790 [   add_application(character varying, character varying, character varying, character varying)    FUNCTION     q  CREATE FUNCTION public.add_application(p_nombre character varying, p_apikey character varying, p_clientsecretkey character varying, p_privatekey character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO aplicaciones (nombre, apikey, clientsecretkey, privatekey)
    VALUES (p_nombre, p_apikey, p_clientsecretkey, p_privatekey);
END;
$$;
 �   DROP FUNCTION public.add_application(p_nombre character varying, p_apikey character varying, p_clientsecretkey character varying, p_privatekey character varying);
       public          postgres    false    4            �            1255    16891 M   add_package(character varying, character varying, character varying, numeric)    FUNCTION     w  CREATE FUNCTION public.add_package(p_tracking_number character varying, p_customer_name character varying, p_delivery_address character varying, p_weight numeric) RETURNS integer
    LANGUAGE plpgsql
    AS $$
DECLARE
    inserted_package_id INTEGER;
BEGIN

    INSERT INTO packages (tracking_number, customer_name, delivery_address, weight)
    VALUES (p_tracking_number, p_customer_name, p_delivery_address, p_weight)
    RETURNING id_package INTO inserted_package_id;


    INSERT INTO package_states  (package_id, state)
    VALUES (inserted_package_id, 'Nuevo'); 
    
     RETURN inserted_package_id;
END;
$$;
 �   DROP FUNCTION public.add_package(p_tracking_number character varying, p_customer_name character varying, p_delivery_address character varying, p_weight numeric);
       public          postgres    false    4            �            1255    16778    eliminar_proyecto(integer) 	   PROCEDURE     �   CREATE PROCEDURE public.eliminar_proyecto(IN p_id_proyecto integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    DELETE FROM proyecto
    WHERE id_proyecto = p_id_proyecto;
END;
$$;
 C   DROP PROCEDURE public.eliminar_proyecto(IN p_id_proyecto integer);
       public          postgres    false    4            �            1255    16886    get_all_packages()    FUNCTION     Y  CREATE FUNCTION public.get_all_packages() RETURNS TABLE(id_package integer, tracking_number character varying, customer_name character varying, delivery_address character varying, state_date_packages timestamp without time zone, weight numeric, state_id integer, id_package_states integer, state text, state_date_estatus timestamp without time zone)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT
        p.id_package,
        p.tracking_number,
        p.customer_name,
        p.delivery_address,
        p.state_date_packages,
        p.weight,
        ps.id_package_states,
        ps.package_id,
        ps.state,
        ps.state_date AS state_date_estatus
    FROM
        packages p
    LEFT JOIN package_states ps ON p.id_package = ps.package_id
    ORDER BY
        p.id_package, ps.state_date DESC;
END;
$$;
 )   DROP FUNCTION public.get_all_packages();
       public          postgres    false    4            �            1255    16822    get_credencials()    FUNCTION     i  CREATE FUNCTION public.get_credencials() RETURNS TABLE(idaplicacion integer, nombre character varying, apikey character varying, clientsecretkey character varying, privatekey character varying, activo boolean)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT * 
    FROM aplicaciones 
    ORDER BY aplicaciones.nombre ASC  ;


END;
$$;
 (   DROP FUNCTION public.get_credencials();
       public          postgres    false    4            �            1255    16820 )   get_credencials_by_parameters(text, text)    FUNCTION     �  CREATE FUNCTION public.get_credencials_by_parameters(p_nombre text, p_apikey text) RETURNS TABLE(idaplicacion integer, nombre character varying, apikey character varying, clientsecretkey character varying, privatekey character varying, activo boolean)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT * 
    FROM aplicaciones 
    WHERE aplicaciones.nombre = p_nombre AND aplicaciones.apikey = p_apiKey
    ORDER BY aplicaciones.nombre ASC  
    LIMIT 1;

END;
$$;
 R   DROP FUNCTION public.get_credencials_by_parameters(p_nombre text, p_apikey text);
       public          postgres    false    4            �            1255    16885 1   get_package_by_tracking_number(character varying)    FUNCTION     �  CREATE FUNCTION public.get_package_by_tracking_number(p_tracking_number character varying) RETURNS TABLE(id_package integer, tracking_number character varying, customer_name character varying, delivery_address character varying, state_date_packages timestamp without time zone, weight numeric, state_id integer, id_package_states integer, state text, state_date_estatus timestamp without time zone)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT
        p.id_package,
        p.tracking_number,
        p.customer_name,
        p.delivery_address,
        p.state_date_packages,
        p.weight,
        ps.id_package_states,
        ps.package_id,
        ps.state,
        ps.state_date AS state_date_estatus
    FROM
        packages p
    LEFT JOIN package_states ps ON p.id_package = ps.package_id
    WHERE
        p.tracking_number = p_tracking_number
    ORDER BY
        p.id_package, ps.state_date DESC;
END;
$$;
 Z   DROP FUNCTION public.get_package_by_tracking_number(p_tracking_number character varying);
       public          postgres    false    4            �            1255    16776 H   insertar_proyecto(integer, character varying, text, date, date, integer) 	   PROCEDURE     �  CREATE PROCEDURE public.insertar_proyecto(IN id_proyecto integer, IN nombre_proyecto character varying, IN descripcion_proyecto text, IN fecha_inicio_proyecto date, IN fecha_fin_proyecto date, IN id_usuario_lider integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO proyecto (nombre, descripcion, fecha_inicio, fecha_fin, id_usuario_lider)
    VALUES (nombre_proyecto, descripcion_proyecto, fecha_inicio_proyecto, fecha_fin_proyecto, id_usuario_lider);
END;
$$;
 �   DROP PROCEDURE public.insertar_proyecto(IN id_proyecto integer, IN nombre_proyecto character varying, IN descripcion_proyecto text, IN fecha_inicio_proyecto date, IN fecha_fin_proyecto date, IN id_usuario_lider integer);
       public          postgres    false    4            �            1255    16786 '   obtener_credenciales(character varying)    FUNCTION     a  CREATE FUNCTION public.obtener_credenciales(nombre_param character varying) RETURNS TABLE(nombre character varying, apikey character varying, apisecret character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT idaplicacion,nombre, apikey, clientsecretkey,privatekey FROM aplicaciones WHERE nombre = nombre_param;
END;
$$;
 K   DROP FUNCTION public.obtener_credenciales(nombre_param character varying);
       public          postgres    false    4            �            1255    16771    obtener_proyectos()    FUNCTION     V  CREATE FUNCTION public.obtener_proyectos() RETURNS TABLE(id_proyecto integer, nombre character varying, descripcion text, fecha_inicio date, fecha_fin date, id_usuario_lider integer, eliminado boolean, fecha_actualizacion timestamp without time zone)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY SELECT * FROM proyecto;
END;
$$;
 *   DROP FUNCTION public.obtener_proyectos();
       public          postgres    false    4            �            1255    16892 F   update_package(integer, character varying, character varying, numeric)    FUNCTION     �  CREATE FUNCTION public.update_package(p_package_id integer, p_customer_name character varying, p_delivery_address character varying, p_weight numeric) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
    UPDATE packages
    SET 
        customer_name = p_customer_name,
        delivery_address = p_delivery_address,
        weight = p_weight
    WHERE
        id_package = p_package_id;

END;
$$;
 �   DROP FUNCTION public.update_package(p_package_id integer, p_customer_name character varying, p_delivery_address character varying, p_weight numeric);
       public          postgres    false    4            �            1259    16802    aplicaciones    TABLE     �   CREATE TABLE public.aplicaciones (
    idaplicacion integer NOT NULL,
    nombre character varying(250),
    apikey character varying(250),
    clientsecretkey character varying(250),
    privatekey character varying(250),
    activo boolean
);
     DROP TABLE public.aplicaciones;
       public         heap    postgres    false    4            �            1259    16801    aplicaciones_idaplicacion_seq    SEQUENCE     �   CREATE SEQUENCE public.aplicaciones_idaplicacion_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 4   DROP SEQUENCE public.aplicaciones_idaplicacion_seq;
       public          postgres    false    218    4                       0    0    aplicaciones_idaplicacion_seq    SEQUENCE OWNED BY     _   ALTER SEQUENCE public.aplicaciones_idaplicacion_seq OWNED BY public.aplicaciones.idaplicacion;
          public          postgres    false    217            �            1259    16866    package_states    TABLE     �   CREATE TABLE public.package_states (
    id_package_states integer NOT NULL,
    package_id integer,
    state text,
    state_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);
 "   DROP TABLE public.package_states;
       public         heap    postgres    false    4            �            1259    16865 $   package_states_id_package_states_seq    SEQUENCE     �   CREATE SEQUENCE public.package_states_id_package_states_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ;   DROP SEQUENCE public.package_states_id_package_states_seq;
       public          postgres    false    222    4                       0    0 $   package_states_id_package_states_seq    SEQUENCE OWNED BY     m   ALTER SEQUENCE public.package_states_id_package_states_seq OWNED BY public.package_states.id_package_states;
          public          postgres    false    221            �            1259    16856    packages    TABLE     (  CREATE TABLE public.packages (
    id_package integer NOT NULL,
    tracking_number character varying(250),
    customer_name character varying(100),
    delivery_address character varying(255),
    state_date_packages timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    weight numeric
);
    DROP TABLE public.packages;
       public         heap    postgres    false    4            �            1259    16855    packages_id_package_seq    SEQUENCE     �   CREATE SEQUENCE public.packages_id_package_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public.packages_id_package_seq;
       public          postgres    false    4    220                        0    0    packages_id_package_seq    SEQUENCE OWNED BY     S   ALTER SEQUENCE public.packages_id_package_seq OWNED BY public.packages.id_package;
          public          postgres    false    219            �            1259    16580    usuario    TABLE     C  CREATE TABLE public.usuario (
    id integer NOT NULL,
    nombre_usuario character varying(50) NOT NULL,
    correo_electronico character varying(100) NOT NULL,
    contrasena character varying(255) NOT NULL,
    fecha_creacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    eliminado boolean DEFAULT false
);
    DROP TABLE public.usuario;
       public         heap    postgres    false    4            �            1259    16579    usuario_id_seq    SEQUENCE     �   CREATE SEQUENCE public.usuario_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.usuario_id_seq;
       public          postgres    false    216    4            !           0    0    usuario_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.usuario_id_seq OWNED BY public.usuario.id;
          public          postgres    false    215            n           2604    16805    aplicaciones idaplicacion    DEFAULT     �   ALTER TABLE ONLY public.aplicaciones ALTER COLUMN idaplicacion SET DEFAULT nextval('public.aplicaciones_idaplicacion_seq'::regclass);
 H   ALTER TABLE public.aplicaciones ALTER COLUMN idaplicacion DROP DEFAULT;
       public          postgres    false    218    217    218            q           2604    16869     package_states id_package_states    DEFAULT     �   ALTER TABLE ONLY public.package_states ALTER COLUMN id_package_states SET DEFAULT nextval('public.package_states_id_package_states_seq'::regclass);
 O   ALTER TABLE public.package_states ALTER COLUMN id_package_states DROP DEFAULT;
       public          postgres    false    222    221    222            o           2604    16859    packages id_package    DEFAULT     z   ALTER TABLE ONLY public.packages ALTER COLUMN id_package SET DEFAULT nextval('public.packages_id_package_seq'::regclass);
 B   ALTER TABLE public.packages ALTER COLUMN id_package DROP DEFAULT;
       public          postgres    false    220    219    220            k           2604    16583 
   usuario id    DEFAULT     h   ALTER TABLE ONLY public.usuario ALTER COLUMN id SET DEFAULT nextval('public.usuario_id_seq'::regclass);
 9   ALTER TABLE public.usuario ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    216    215    216                      0    16802    aplicaciones 
   TABLE DATA           i   COPY public.aplicaciones (idaplicacion, nombre, apikey, clientsecretkey, privatekey, activo) FROM stdin;
    public          postgres    false    218   	Q                 0    16866    package_states 
   TABLE DATA           Z   COPY public.package_states (id_package_states, package_id, state, state_date) FROM stdin;
    public          postgres    false    222   �Q                 0    16856    packages 
   TABLE DATA           }   COPY public.packages (id_package, tracking_number, customer_name, delivery_address, state_date_packages, weight) FROM stdin;
    public          postgres    false    220   �S                 0    16580    usuario 
   TABLE DATA           p   COPY public.usuario (id, nombre_usuario, correo_electronico, contrasena, fecha_creacion, eliminado) FROM stdin;
    public          postgres    false    216   DY       "           0    0    aplicaciones_idaplicacion_seq    SEQUENCE SET     K   SELECT pg_catalog.setval('public.aplicaciones_idaplicacion_seq', 1, true);
          public          postgres    false    217            #           0    0 $   package_states_id_package_states_seq    SEQUENCE SET     S   SELECT pg_catalog.setval('public.package_states_id_package_states_seq', 40, true);
          public          postgres    false    221            $           0    0    packages_id_package_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.packages_id_package_seq', 37, true);
          public          postgres    false    219            %           0    0    usuario_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.usuario_id_seq', 3, true);
          public          postgres    false    215            z           2606    16809    aplicaciones aplicaciones_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public.aplicaciones
    ADD CONSTRAINT aplicaciones_pkey PRIMARY KEY (idaplicacion);
 H   ALTER TABLE ONLY public.aplicaciones DROP CONSTRAINT aplicaciones_pkey;
       public            postgres    false    218            ~           2606    16874 "   package_states package_states_pkey 
   CONSTRAINT     o   ALTER TABLE ONLY public.package_states
    ADD CONSTRAINT package_states_pkey PRIMARY KEY (id_package_states);
 L   ALTER TABLE ONLY public.package_states DROP CONSTRAINT package_states_pkey;
       public            postgres    false    222            |           2606    16864    packages packages_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.packages
    ADD CONSTRAINT packages_pkey PRIMARY KEY (id_package);
 @   ALTER TABLE ONLY public.packages DROP CONSTRAINT packages_pkey;
       public            postgres    false    220            t           2606    16591 &   usuario usuario_correo_electronico_key 
   CONSTRAINT     o   ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_correo_electronico_key UNIQUE (correo_electronico);
 P   ALTER TABLE ONLY public.usuario DROP CONSTRAINT usuario_correo_electronico_key;
       public            postgres    false    216            v           2606    16589 "   usuario usuario_nombre_usuario_key 
   CONSTRAINT     g   ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_nombre_usuario_key UNIQUE (nombre_usuario);
 L   ALTER TABLE ONLY public.usuario DROP CONSTRAINT usuario_nombre_usuario_key;
       public            postgres    false    216            x           2606    16587    usuario usuario_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.usuario DROP CONSTRAINT usuario_pkey;
       public            postgres    false    216                       2606    16875 -   package_states package_states_package_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.package_states
    ADD CONSTRAINT package_states_package_id_fkey FOREIGN KEY (package_id) REFERENCES public.packages(id_package) ON DELETE CASCADE;
 W   ALTER TABLE ONLY public.package_states DROP CONSTRAINT package_states_package_id_fkey;
       public          postgres    false    222    4732    220               �   x����0 й�C���1�Ip`9�5A4A������Sx'���(M9`î �k�����S}�vs��{��-�z٦O��޼��ᑪ˜5�QXB0h�	X�18E�\��D�e�R��$�         %  x�u�;n[AEk�*�����O��b!�(�-{?.Sg	�X�<	pS��t@rxH
	}>��O���������|b|��1��%�B���o�_�wFw��6�6��t���+����RQH�����W�c*�[�Fv�f@�z�
k�ۻƕy:����ߠ^����?Y��vj������N}�8�I<�2��$L=�dr���~-���Ԙ�s���rH)��������H��L��ּ&���w0C��C��~��_���=I'�0C7FV�Q$� ��6��>�4'�Z�fAw-Km���L���Z���[R!�������ލ%)f:r�a�no�U���1�*�r�%�lG���MTԓb�#	}j���gVC�FI��|YzC���4��7���fH:H#	�5wi�0'�, �շ��Ē1id�ߔB��,��6�6,\S��t}=�&3�@�Bp�0Z�d�,�����Z�OV�	��"�h������TL��14�L a][S�	x(NM A��1�[A{2'$,l�ڤ"���d�K0^�w�P+�}�)�?�nFa         d  x���Ir���٧��"�9ԎABa*d�6A����j�l(Р�������*�!GU����|��ҙ뷎�|{��iz}�^����izgȒ����D�h�9�cO��L�XQo�ͧ>��t����u�����<�cp�ɼ�x�}Z͟oN_�w��gƇ����_}<��]����?���Ƽ>��Z����ij�}}����EtqiV���qv�C��4�X]�fq�����fq6�O�Y��m��1H\��u�*�6
\o� �|n�|�`��Y 
��9�{�P���"){_-u)���حȗ�<}�?���/���O����ټ�=��ǻ$��|��ɩ\�f��0k�>)l��#S8�6bf��S�|��P,�9�y�S��<Ý�F��<��-T���Í������� ��ݝi5��w�owK��h�~���TkS�\,��΂�q� ��R㨡'�I�/i_��jf�7S�|�~��"=��H�8S��Qõ��U;nbYK���z��u8������s��������9Dou�z}�1{I�	I�SM�eR��kc/��K��7�\@�ƳS/�\0�I��j�Rl�NSL#�a������o?\}���z�M���Z$+6�R��J�hK�`[�aki��=��%������������<?5���m
l�j"Ͷ��`xY�-6��*d��/Ml��Op�7�x������-Tg�))��'k�E*{gGHO�[C�P�u���7M{��W�WO��|�*Z��jCP�YrF;
.��X��^�j9��B�{�*��Z[�"#�w����@�J���&�8�%��)�W\��zH���R�팤��m�d�k4rn6�4d�1a�Sz$�m�h�d�E���T7���\�xZ�Dha��*疹u��#cl���U�Ҍ�=����NyX��������#���F:�h�y#��bxDC�ѱf�$;��
�0�]�	��3�`*bQ��$��]M5P�b}��e�lΗ�M�:Q�Bѐ��ژ�b��#�r��Br/�#�4$qḐG�DCN����a��%��ŧ����l���6��V|��=�������a��dS3FE�Jm(�je���B�A��qq�,X��I!�[��j��U�j%�N�nN���v���Y�_�E�h�FF%q�8J&hD��h��K����ΐ>G(yڽ�F��XѬ\!D*�ְ��P�V�z���u�b���ˎ[M�m�xe�:���|�~HV�!�2I���W������s	��,d@����h��5Jy�N�FFe���4
� �\ԯ}HFT4*v',%b��(��
�����k�q0%��P��	R�dRЂE�t���������o�6'�����/L���Ǵ�����?����         c   x�3�,-.M,��7�3R+srR���s9���J��S�9��LtLt�Lͭ��,�̌��9Ӹ�`��7����a�1~�b���� ��@�     