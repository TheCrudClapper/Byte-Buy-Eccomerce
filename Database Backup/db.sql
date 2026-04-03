--
-- PostgreSQL database dump
--

\restrict 5hVgM3Od0QperTuyYaXf119DnS8i4tBnPZkmFPN83T9WQvpgyYiZWEFn8xdSna0

-- Dumped from database version 17.7
-- Dumped by pg_dump version 17.7

-- Started on 2026-02-21 17:49:18

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 6 (class 2615 OID 27382)
-- Name: hangfire; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA hangfire;


ALTER SCHEMA hangfire OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 275 (class 1259 OID 27674)
-- Name: aggregatedcounter; Type: TABLE; Schema: hangfire; Owner: postgres
--

CREATE TABLE hangfire.aggregatedcounter (
    id bigint NOT NULL,
    key text NOT NULL,
    value bigint NOT NULL,
    expireat timestamp with time zone
);


ALTER TABLE hangfire.aggregatedcounter OWNER TO postgres;

--
-- TOC entry 274 (class 1259 OID 27673)
-- Name: aggregatedcounter_id_seq; Type: SEQUENCE; Schema: hangfire; Owner: postgres
--

CREATE SEQUENCE hangfire.aggregatedcounter_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE hangfire.aggregatedcounter_id_seq OWNER TO postgres;

--
-- TOC entry 5316 (class 0 OID 0)
-- Dependencies: 274
-- Name: aggregatedcounter_id_seq; Type: SEQUENCE OWNED BY; Schema: hangfire; Owner: postgres
--

ALTER SEQUENCE hangfire.aggregatedcounter_id_seq OWNED BY hangfire.aggregatedcounter.id;


--
-- TOC entry 257 (class 1259 OID 27389)
-- Name: counter; Type: TABLE; Schema: hangfire; Owner: postgres
--

CREATE TABLE hangfire.counter (
    id bigint NOT NULL,
    key text NOT NULL,
    value bigint NOT NULL,
    expireat timestamp with time zone
);


ALTER TABLE hangfire.counter OWNER TO postgres;

--
-- TOC entry 256 (class 1259 OID 27388)
-- Name: counter_id_seq; Type: SEQUENCE; Schema: hangfire; Owner: postgres
--

CREATE SEQUENCE hangfire.counter_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE hangfire.counter_id_seq OWNER TO postgres;

--
-- TOC entry 5317 (class 0 OID 0)
-- Dependencies: 256
-- Name: counter_id_seq; Type: SEQUENCE OWNED BY; Schema: hangfire; Owner: postgres
--

ALTER SEQUENCE hangfire.counter_id_seq OWNED BY hangfire.counter.id;


--
-- TOC entry 259 (class 1259 OID 27397)
-- Name: hash; Type: TABLE; Schema: hangfire; Owner: postgres
--

CREATE TABLE hangfire.hash (
    id bigint NOT NULL,
    key text NOT NULL,
    field text NOT NULL,
    value text,
    expireat timestamp with time zone,
    updatecount integer DEFAULT 0 NOT NULL
);


ALTER TABLE hangfire.hash OWNER TO postgres;

--
-- TOC entry 258 (class 1259 OID 27396)
-- Name: hash_id_seq; Type: SEQUENCE; Schema: hangfire; Owner: postgres
--

CREATE SEQUENCE hangfire.hash_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE hangfire.hash_id_seq OWNER TO postgres;

--
-- TOC entry 5318 (class 0 OID 0)
-- Dependencies: 258
-- Name: hash_id_seq; Type: SEQUENCE OWNED BY; Schema: hangfire; Owner: postgres
--

ALTER SEQUENCE hangfire.hash_id_seq OWNED BY hangfire.hash.id;


--
-- TOC entry 261 (class 1259 OID 27408)
-- Name: job; Type: TABLE; Schema: hangfire; Owner: postgres
--

CREATE TABLE hangfire.job (
    id bigint NOT NULL,
    stateid bigint,
    statename text,
    invocationdata jsonb NOT NULL,
    arguments jsonb NOT NULL,
    createdat timestamp with time zone NOT NULL,
    expireat timestamp with time zone,
    updatecount integer DEFAULT 0 NOT NULL
);


ALTER TABLE hangfire.job OWNER TO postgres;

--
-- TOC entry 260 (class 1259 OID 27407)
-- Name: job_id_seq; Type: SEQUENCE; Schema: hangfire; Owner: postgres
--

CREATE SEQUENCE hangfire.job_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE hangfire.job_id_seq OWNER TO postgres;

--
-- TOC entry 5319 (class 0 OID 0)
-- Dependencies: 260
-- Name: job_id_seq; Type: SEQUENCE OWNED BY; Schema: hangfire; Owner: postgres
--

ALTER SEQUENCE hangfire.job_id_seq OWNED BY hangfire.job.id;


--
-- TOC entry 272 (class 1259 OID 27468)
-- Name: jobparameter; Type: TABLE; Schema: hangfire; Owner: postgres
--

CREATE TABLE hangfire.jobparameter (
    id bigint NOT NULL,
    jobid bigint NOT NULL,
    name text NOT NULL,
    value text,
    updatecount integer DEFAULT 0 NOT NULL
);


ALTER TABLE hangfire.jobparameter OWNER TO postgres;

--
-- TOC entry 271 (class 1259 OID 27467)
-- Name: jobparameter_id_seq; Type: SEQUENCE; Schema: hangfire; Owner: postgres
--

CREATE SEQUENCE hangfire.jobparameter_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE hangfire.jobparameter_id_seq OWNER TO postgres;

--
-- TOC entry 5320 (class 0 OID 0)
-- Dependencies: 271
-- Name: jobparameter_id_seq; Type: SEQUENCE OWNED BY; Schema: hangfire; Owner: postgres
--

ALTER SEQUENCE hangfire.jobparameter_id_seq OWNED BY hangfire.jobparameter.id;


--
-- TOC entry 265 (class 1259 OID 27433)
-- Name: jobqueue; Type: TABLE; Schema: hangfire; Owner: postgres
--

CREATE TABLE hangfire.jobqueue (
    id bigint NOT NULL,
    jobid bigint NOT NULL,
    queue text NOT NULL,
    fetchedat timestamp with time zone,
    updatecount integer DEFAULT 0 NOT NULL
);


ALTER TABLE hangfire.jobqueue OWNER TO postgres;

--
-- TOC entry 264 (class 1259 OID 27432)
-- Name: jobqueue_id_seq; Type: SEQUENCE; Schema: hangfire; Owner: postgres
--

CREATE SEQUENCE hangfire.jobqueue_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE hangfire.jobqueue_id_seq OWNER TO postgres;

--
-- TOC entry 5321 (class 0 OID 0)
-- Dependencies: 264
-- Name: jobqueue_id_seq; Type: SEQUENCE OWNED BY; Schema: hangfire; Owner: postgres
--

ALTER SEQUENCE hangfire.jobqueue_id_seq OWNED BY hangfire.jobqueue.id;


--
-- TOC entry 267 (class 1259 OID 27441)
-- Name: list; Type: TABLE; Schema: hangfire; Owner: postgres
--

CREATE TABLE hangfire.list (
    id bigint NOT NULL,
    key text NOT NULL,
    value text,
    expireat timestamp with time zone,
    updatecount integer DEFAULT 0 NOT NULL
);


ALTER TABLE hangfire.list OWNER TO postgres;

--
-- TOC entry 266 (class 1259 OID 27440)
-- Name: list_id_seq; Type: SEQUENCE; Schema: hangfire; Owner: postgres
--

CREATE SEQUENCE hangfire.list_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE hangfire.list_id_seq OWNER TO postgres;

--
-- TOC entry 5322 (class 0 OID 0)
-- Dependencies: 266
-- Name: list_id_seq; Type: SEQUENCE OWNED BY; Schema: hangfire; Owner: postgres
--

ALTER SEQUENCE hangfire.list_id_seq OWNED BY hangfire.list.id;


--
-- TOC entry 273 (class 1259 OID 27482)
-- Name: lock; Type: TABLE; Schema: hangfire; Owner: postgres
--

CREATE TABLE hangfire.lock (
    resource text NOT NULL,
    updatecount integer DEFAULT 0 NOT NULL,
    acquired timestamp with time zone
);


ALTER TABLE hangfire.lock OWNER TO postgres;

--
-- TOC entry 255 (class 1259 OID 27383)
-- Name: schema; Type: TABLE; Schema: hangfire; Owner: postgres
--

CREATE TABLE hangfire.schema (
    version integer NOT NULL
);


ALTER TABLE hangfire.schema OWNER TO postgres;

--
-- TOC entry 268 (class 1259 OID 27449)
-- Name: server; Type: TABLE; Schema: hangfire; Owner: postgres
--

CREATE TABLE hangfire.server (
    id text NOT NULL,
    data jsonb,
    lastheartbeat timestamp with time zone NOT NULL,
    updatecount integer DEFAULT 0 NOT NULL
);


ALTER TABLE hangfire.server OWNER TO postgres;

--
-- TOC entry 270 (class 1259 OID 27457)
-- Name: set; Type: TABLE; Schema: hangfire; Owner: postgres
--

CREATE TABLE hangfire.set (
    id bigint NOT NULL,
    key text NOT NULL,
    score double precision NOT NULL,
    value text NOT NULL,
    expireat timestamp with time zone,
    updatecount integer DEFAULT 0 NOT NULL
);


ALTER TABLE hangfire.set OWNER TO postgres;

--
-- TOC entry 269 (class 1259 OID 27456)
-- Name: set_id_seq; Type: SEQUENCE; Schema: hangfire; Owner: postgres
--

CREATE SEQUENCE hangfire.set_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE hangfire.set_id_seq OWNER TO postgres;

--
-- TOC entry 5323 (class 0 OID 0)
-- Dependencies: 269
-- Name: set_id_seq; Type: SEQUENCE OWNED BY; Schema: hangfire; Owner: postgres
--

ALTER SEQUENCE hangfire.set_id_seq OWNED BY hangfire.set.id;


--
-- TOC entry 263 (class 1259 OID 27418)
-- Name: state; Type: TABLE; Schema: hangfire; Owner: postgres
--

CREATE TABLE hangfire.state (
    id bigint NOT NULL,
    jobid bigint NOT NULL,
    name text NOT NULL,
    reason text,
    createdat timestamp with time zone NOT NULL,
    data jsonb,
    updatecount integer DEFAULT 0 NOT NULL
);


ALTER TABLE hangfire.state OWNER TO postgres;

--
-- TOC entry 262 (class 1259 OID 27417)
-- Name: state_id_seq; Type: SEQUENCE; Schema: hangfire; Owner: postgres
--

CREATE SEQUENCE hangfire.state_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE hangfire.state_id_seq OWNER TO postgres;

--
-- TOC entry 5324 (class 0 OID 0)
-- Dependencies: 262
-- Name: state_id_seq; Type: SEQUENCE OWNED BY; Schema: hangfire; Owner: postgres
--

ALTER SEQUENCE hangfire.state_id_seq OWNED BY hangfire.state.id;


--
-- TOC entry 218 (class 1259 OID 26820)
-- Name: AspNetRoleClaims; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AspNetRoleClaims" (
    "Id" integer NOT NULL,
    "RoleId" uuid NOT NULL,
    "ClaimType" text,
    "ClaimValue" text
);


ALTER TABLE public."AspNetRoleClaims" OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 26825)
-- Name: AspNetRoleClaims_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."AspNetRoleClaims" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."AspNetRoleClaims_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 220 (class 1259 OID 26826)
-- Name: AspNetRoles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AspNetRoles" (
    "Id" uuid NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone,
    "DateDeleted" timestamp with time zone,
    "Name" character varying(256),
    "NormalizedName" character varying(256),
    "ConcurrencyStamp" text,
    "IsSystemRole" boolean DEFAULT false NOT NULL
);


ALTER TABLE public."AspNetRoles" OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 26831)
-- Name: AspNetUserClaims; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AspNetUserClaims" (
    "Id" integer NOT NULL,
    "UserId" uuid NOT NULL,
    "ClaimType" text,
    "ClaimValue" text
);


ALTER TABLE public."AspNetUserClaims" OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 26836)
-- Name: AspNetUserClaims_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."AspNetUserClaims" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."AspNetUserClaims_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 223 (class 1259 OID 26837)
-- Name: AspNetUserLogins; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AspNetUserLogins" (
    "LoginProvider" text NOT NULL,
    "ProviderKey" text NOT NULL,
    "ProviderDisplayName" text,
    "UserId" uuid NOT NULL
);


ALTER TABLE public."AspNetUserLogins" OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 26842)
-- Name: AspNetUserRoles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AspNetUserRoles" (
    "UserId" uuid NOT NULL,
    "RoleId" uuid NOT NULL
);


ALTER TABLE public."AspNetUserRoles" OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 26845)
-- Name: AspNetUserTokens; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AspNetUserTokens" (
    "UserId" uuid NOT NULL,
    "LoginProvider" text NOT NULL,
    "Name" text NOT NULL,
    "Value" text
);


ALTER TABLE public."AspNetUserTokens" OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 26850)
-- Name: AspNetUsers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AspNetUsers" (
    "Id" uuid NOT NULL,
    "FirstName" character varying(50) NOT NULL,
    "LastName" character varying(50) NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone,
    "DateDeleted" timestamp with time zone,
    "UserName" character varying(256),
    "NormalizedUserName" character varying(256),
    "Email" character varying(256),
    "NormalizedEmail" character varying(256),
    "EmailConfirmed" boolean NOT NULL,
    "PasswordHash" text,
    "SecurityStamp" text,
    "ConcurrencyStamp" text,
    "PhoneNumber" text,
    "PhoneNumberConfirmed" boolean NOT NULL,
    "TwoFactorEnabled" boolean NOT NULL,
    "LockoutEnd" timestamp with time zone,
    "LockoutEnabled" boolean NOT NULL,
    "AccessFailedCount" integer NOT NULL,
    "HomeAddress_City" character varying(50),
    "HomeAddress_Country" character varying(50),
    "HomeAddress_FlatNumber" character varying(10),
    "HomeAddress_HouseNumber" character varying(10),
    "HomeAddress_PostalCode" character varying(20),
    "HomeAddress_Street" character varying(50),
    "UserType" character varying(21) DEFAULT ''::character varying NOT NULL,
    "HomeAddress_PostalCity" character varying(50),
    "CartId" uuid,
    "CompanyId" uuid
);


ALTER TABLE public."AspNetUsers" OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 26856)
-- Name: CartOffers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."CartOffers" (
    "Id" uuid NOT NULL,
    "Quantity" integer NOT NULL,
    "CartId" uuid NOT NULL,
    "OfferId" uuid NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone
);


ALTER TABLE public."CartOffers" OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 26859)
-- Name: Carts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Carts" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "TotalCartValue_Amount" numeric(18,3) NOT NULL,
    "TotalCartValue_Currency" character varying(3) NOT NULL,
    "TotalItemsValue_Amount" numeric(18,3) NOT NULL,
    "TotalItemsValue_Currency" character varying(3) NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone
);


ALTER TABLE public."Carts" OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 26862)
-- Name: Categories; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Categories" (
    "Id" uuid NOT NULL,
    "Name" character varying(20) NOT NULL,
    "Description" character varying(50),
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone
);


ALTER TABLE public."Categories" OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 26865)
-- Name: Company; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Company" (
    "Id" uuid NOT NULL,
    "CompanyName" character varying(50) NOT NULL,
    "TIN" character varying(20) NOT NULL,
    "CompanyAddress_Street" character varying(50) NOT NULL,
    "CompanyAddress_HouseNumber" character varying(20) NOT NULL,
    "CompanyAddress_PostalCode" character varying(50) NOT NULL,
    "CompanyAddress_PostalCity" character varying(50) NOT NULL,
    "CompanyAddress_City" character varying(50) NOT NULL,
    "CompanyAddress_Country" character varying(50) NOT NULL,
    "CompanyAddress_FlatNumber" text,
    "Email" character varying(50) NOT NULL,
    "Phone" character varying(16) NOT NULL,
    "Slogan" character varying(30) NOT NULL,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone
);


ALTER TABLE public."Company" OWNER TO postgres;

--
-- TOC entry 231 (class 1259 OID 26870)
-- Name: Conditions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Conditions" (
    "Id" uuid NOT NULL,
    "Name" character varying(20) NOT NULL,
    "Description" character varying(50),
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone
);


ALTER TABLE public."Conditions" OWNER TO postgres;

--
-- TOC entry 232 (class 1259 OID 26873)
-- Name: Countries; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Countries" (
    "Id" uuid NOT NULL,
    "Name" character varying(50) NOT NULL,
    "Code" character varying(3) NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone
);


ALTER TABLE public."Countries" OWNER TO postgres;

--
-- TOC entry 233 (class 1259 OID 26876)
-- Name: Deliveries; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Deliveries" (
    "Id" uuid NOT NULL,
    "Name" character varying(50) NOT NULL,
    "Description" character varying(50),
    "Price_Amount" numeric(18,3) DEFAULT 0.0 NOT NULL,
    "Price_Currency" character varying(3) DEFAULT ''::character varying NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone,
    "Channel" character varying(30) DEFAULT ''::character varying NOT NULL,
    "ParcelSize" character varying(30),
    "DeliveryCarrierId" uuid DEFAULT '00000000-0000-0000-0000-000000000000'::uuid NOT NULL
);


ALTER TABLE public."Deliveries" OWNER TO postgres;

--
-- TOC entry 234 (class 1259 OID 26883)
-- Name: DeliveryCarriers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."DeliveryCarriers" (
    "Id" uuid NOT NULL,
    "Name" character varying(50) NOT NULL,
    "Code" character varying(20) NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone
);


ALTER TABLE public."DeliveryCarriers" OWNER TO postgres;

--
-- TOC entry 235 (class 1259 OID 26886)
-- Name: Images; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Images" (
    "Id" uuid NOT NULL,
    "ImagePath" text NOT NULL,
    "ItemId" uuid NOT NULL,
    "AltText" character varying(50),
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone
);


ALTER TABLE public."Images" OWNER TO postgres;

--
-- TOC entry 236 (class 1259 OID 26891)
-- Name: Items; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Items" (
    "Id" uuid NOT NULL,
    "Name" character varying(75) NOT NULL,
    "Description" character varying(2000) NOT NULL,
    "CategoryId" uuid NOT NULL,
    "ConditionId" uuid NOT NULL,
    "StockQuantity" integer NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone,
    "IsCompanyItem" boolean DEFAULT false NOT NULL
);


ALTER TABLE public."Items" OWNER TO postgres;

--
-- TOC entry 237 (class 1259 OID 26897)
-- Name: OfferDeliveries; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."OfferDeliveries" (
    "Id" uuid NOT NULL,
    "OfferId" uuid NOT NULL,
    "DeliveryId" uuid NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone
);


ALTER TABLE public."OfferDeliveries" OWNER TO postgres;

--
-- TOC entry 238 (class 1259 OID 26900)
-- Name: Offers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Offers" (
    "Id" uuid NOT NULL,
    "ItemId" uuid NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone,
    "CreatedByUserId" uuid DEFAULT '00000000-0000-0000-0000-000000000000'::uuid NOT NULL,
    "QuantityAvailable" integer DEFAULT 0 NOT NULL,
    "OfferAddressSnapshot_City" character varying(50) DEFAULT ''::character varying NOT NULL,
    "OfferAddressSnapshot_Country" character varying(50) DEFAULT ''::character varying NOT NULL,
    "OfferAddressSnapshot_FlatNumber" character varying(10),
    "OfferAddressSnapshot_HouseNumber" character varying(10) DEFAULT ''::character varying NOT NULL,
    "OfferAddressSnapshot_PostalCity" character varying(50) DEFAULT ''::character varying NOT NULL,
    "OfferAddressSnapshot_PostalCode" character varying(20) DEFAULT ''::character varying NOT NULL,
    "OfferAddressSnapshot_Street" character varying(50) DEFAULT ''::character varying NOT NULL,
    "Seller_Id" uuid DEFAULT '00000000-0000-0000-0000-000000000000'::uuid NOT NULL,
    "Seller_Type" character varying(20) DEFAULT ''::character varying NOT NULL,
    "Status" character varying(20) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public."Offers" OWNER TO postgres;

--
-- TOC entry 239 (class 1259 OID 26907)
-- Name: OrderDeliveries; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."OrderDeliveries" (
    "Id" uuid NOT NULL,
    "OrderId" uuid NOT NULL,
    "DeliveryName" character varying(50) NOT NULL,
    "CarrierCode" character varying(20) NOT NULL,
    "Channel" character varying(20) NOT NULL,
    "Price_Amount" numeric(18,3) NOT NULL,
    "Price_Currency" character varying(3) NOT NULL,
    "Street" character varying(50),
    "HouseNumber" character varying(20),
    "FlatNumber" character varying(10),
    "City" character varying(50),
    "PostalCity" character varying(50),
    "PostalCode" character varying(20),
    "PickupPointId" text,
    "PickupPointName" character varying(50),
    "PickupStreet" character varying(50),
    "PickupCity" character varying(50),
    "PickupLocalNumber" character varying(20),
    "ParcelLockerId" text,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone
);


ALTER TABLE public."OrderDeliveries" OWNER TO postgres;

--
-- TOC entry 240 (class 1259 OID 26915)
-- Name: OrderLines; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."OrderLines" (
    "Id" uuid NOT NULL,
    "OrderId" uuid NOT NULL,
    "ItemName" text NOT NULL,
    "Thumbnail_ImagePath" text NOT NULL,
    "Thumbnail_AltText" character varying(50),
    "Quantity" integer NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "OrderLineType" character varying(13) NOT NULL,
    "PricePerDay_Amount" numeric(18,3),
    "PricePerDay_Currency" character varying(3),
    "RentalDays" integer,
    "PricePerItem_Amount" numeric(18,3),
    "PricePerItem_Currency" character varying(3),
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone,
    "OfferId" uuid DEFAULT '00000000-0000-0000-0000-000000000000'::uuid NOT NULL
);


ALTER TABLE public."OrderLines" OWNER TO postgres;

--
-- TOC entry 241 (class 1259 OID 26920)
-- Name: Orders; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Orders" (
    "Id" uuid NOT NULL,
    "Total_Amount" numeric(18,3) NOT NULL,
    "Total_Currency" character varying(3) NOT NULL,
    "Status" character varying(20) NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone,
    "SellerSnapshot_Address_City" character varying(50) DEFAULT ''::character varying NOT NULL,
    "SellerSnapshot_Address_Country" character varying(50) DEFAULT ''::character varying NOT NULL,
    "SellerSnapshot_Address_FlatNumber" character varying(10),
    "SellerSnapshot_Address_HouseNumber" character varying(10) DEFAULT ''::character varying NOT NULL,
    "SellerSnapshot_Address_PostalCode" character varying(20) DEFAULT ''::character varying NOT NULL,
    "SellerSnapshot_Address_Street" character varying(50) DEFAULT ''::character varying NOT NULL,
    "SellerSnapshot_Address_PostalCity" character varying(50) DEFAULT ''::character varying NOT NULL,
    "BuyerId" uuid DEFAULT '00000000-0000-0000-0000-000000000000'::uuid NOT NULL,
    "DateDelivered" timestamp with time zone,
    "LinesTotal_Amount" numeric(18,3) DEFAULT 0.0 NOT NULL,
    "LinesTotal_Currency" character varying(3) DEFAULT ''::character varying NOT NULL,
    "SellerSnapshot_DisplayName" character varying(100) DEFAULT ''::character varying NOT NULL,
    "SellerSnapshot_SellerId" uuid DEFAULT '00000000-0000-0000-0000-000000000000'::uuid NOT NULL,
    "SellerSnapshot_TIN" character varying(20),
    "SellerSnapshot_Type" character varying(20) DEFAULT ''::character varying NOT NULL,
    "BuyerSnapshot_Address_City" character varying(50) DEFAULT ''::character varying NOT NULL,
    "BuyerSnapshot_Address_Country" character varying(50) DEFAULT ''::character varying NOT NULL,
    "BuyerSnapshot_Address_FlatNumber" character varying(10),
    "BuyerSnapshot_Address_HouseNumber" character varying(10) DEFAULT ''::character varying NOT NULL,
    "BuyerSnapshot_Address_PostalCity" character varying(50) DEFAULT ''::character varying NOT NULL,
    "BuyerSnapshot_Address_PostalCode" character varying(20) DEFAULT ''::character varying NOT NULL,
    "BuyerSnapshot_Address_Street" character varying(50) DEFAULT ''::character varying NOT NULL,
    "BuyerSnapshot_Email" character varying(150) DEFAULT ''::character varying NOT NULL,
    "BuyerSnapshot_FullName" character varying(150) DEFAULT ''::character varying NOT NULL,
    "BuyerSnapshot_PhoneNumber" character varying(15)
);


ALTER TABLE public."Orders" OWNER TO postgres;

--
-- TOC entry 254 (class 1259 OID 27324)
-- Name: PaymentDetails; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."PaymentDetails" (
    "Id" uuid NOT NULL,
    "PaymentId" uuid NOT NULL,
    "Method" integer NOT NULL,
    "PhoneNumber" character varying(15),
    "MaskedCardNumber" character varying(19),
    "CardHolderName" character varying(30),
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone,
    "DateDeleted" timestamp with time zone,
    "IsActive" boolean DEFAULT false NOT NULL
);


ALTER TABLE public."PaymentDetails" OWNER TO postgres;

--
-- TOC entry 242 (class 1259 OID 26939)
-- Name: PaymentOrders; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."PaymentOrders" (
    "Id" uuid NOT NULL,
    "PaymentId" uuid NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone,
    "Amount_Amount" numeric(18,3) DEFAULT 0.0 NOT NULL,
    "Amount_Currency" character varying(3) DEFAULT ''::character varying NOT NULL,
    "OrderId" uuid DEFAULT '00000000-0000-0000-0000-000000000000'::uuid NOT NULL
);


ALTER TABLE public."PaymentOrders" OWNER TO postgres;

--
-- TOC entry 243 (class 1259 OID 26942)
-- Name: Payments; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Payments" (
    "Id" uuid NOT NULL,
    "Status" character varying(20) NOT NULL,
    "Amount_Currency" character varying(3) NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone,
    "Amount_Amount" numeric(18,3) DEFAULT 0.0 NOT NULL,
    "ExternalTransactionId" text,
    "Method" character varying(20) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public."Payments" OWNER TO postgres;

--
-- TOC entry 244 (class 1259 OID 26949)
-- Name: Permissions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Permissions" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text DEFAULT ''::text NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone
);


ALTER TABLE public."Permissions" OWNER TO postgres;

--
-- TOC entry 245 (class 1259 OID 26954)
-- Name: RentCartOffers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."RentCartOffers" (
    "Id" uuid NOT NULL,
    "RentalDays" integer DEFAULT 0 NOT NULL
);


ALTER TABLE public."RentCartOffers" OWNER TO postgres;

--
-- TOC entry 246 (class 1259 OID 26958)
-- Name: RentOffers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."RentOffers" (
    "Id" uuid NOT NULL,
    "PricePerDay_Amount" numeric(18,3) NOT NULL,
    "PricePerDay_Currency" character varying(3) NOT NULL,
    "MaxRentalDays" integer NOT NULL
);


ALTER TABLE public."RentOffers" OWNER TO postgres;

--
-- TOC entry 247 (class 1259 OID 26961)
-- Name: Rentals; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Rentals" (
    "Id" uuid NOT NULL,
    "RentOrderLineId" uuid NOT NULL,
    "RentalStartDate" timestamp with time zone NOT NULL,
    "RentalEndDate" timestamp with time zone,
    "ReturnedDate" timestamp with time zone,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone,
    "BorrowerId" uuid DEFAULT '00000000-0000-0000-0000-000000000000'::uuid NOT NULL,
    "Lender_SellerId" uuid DEFAULT '00000000-0000-0000-0000-000000000000'::uuid NOT NULL,
    "Lender_Type" character varying(20) DEFAULT ''::character varying NOT NULL,
    "PricePerDay_Amount" numeric(18,3) DEFAULT 0.0 NOT NULL,
    "PricePerDay_Currency" character varying(3) DEFAULT ''::character varying NOT NULL,
    "RentalDays" integer DEFAULT 0 NOT NULL,
    "Status" integer DEFAULT 0 NOT NULL,
    "DeliveryDate" timestamp with time zone DEFAULT '-infinity'::timestamp with time zone NOT NULL,
    "ItemName" text DEFAULT ''::text NOT NULL,
    "Lender_Address_City" character varying(50) DEFAULT ''::character varying NOT NULL,
    "Lender_Address_Country" character varying(50) DEFAULT ''::character varying NOT NULL,
    "Lender_Address_FlatNumber" character varying(10),
    "Lender_Address_HouseNumber" character varying(10) DEFAULT ''::character varying NOT NULL,
    "Lender_Address_PostalCity" character varying(50) DEFAULT ''::character varying NOT NULL,
    "Lender_Address_PostalCode" character varying(20) DEFAULT ''::character varying NOT NULL,
    "Lender_Address_Street" character varying(50) DEFAULT ''::character varying NOT NULL,
    "Lender_DisplayName" character varying(100) DEFAULT ''::character varying NOT NULL,
    "Lender_TIN" character varying(20),
    "Quantity" integer DEFAULT 0 NOT NULL,
    "Thumbnail_AltText" character varying(50),
    "Thumbnail_ImagePath" text DEFAULT ''::text NOT NULL
);


ALTER TABLE public."Rentals" OWNER TO postgres;

--
-- TOC entry 248 (class 1259 OID 26971)
-- Name: RolePermissions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."RolePermissions" (
    "Id" uuid NOT NULL,
    "RoleId" uuid NOT NULL,
    "PermissionId" uuid NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone
);


ALTER TABLE public."RolePermissions" OWNER TO postgres;

--
-- TOC entry 249 (class 1259 OID 26974)
-- Name: SaleCartOffers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."SaleCartOffers" (
    "Id" uuid NOT NULL
);


ALTER TABLE public."SaleCartOffers" OWNER TO postgres;

--
-- TOC entry 250 (class 1259 OID 26977)
-- Name: SaleOffers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."SaleOffers" (
    "Id" uuid NOT NULL,
    "PricePerItem_Amount" numeric(18,3) NOT NULL,
    "PricePerItem_Currency" character varying(3) NOT NULL
);


ALTER TABLE public."SaleOffers" OWNER TO postgres;

--
-- TOC entry 251 (class 1259 OID 26980)
-- Name: ShippingAddresses; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ShippingAddresses" (
    "Id" uuid NOT NULL,
    "Street" character varying(50) NOT NULL,
    "HouseNumber" character varying(20) NOT NULL,
    "PostalCity" character varying(50) NOT NULL,
    "PostalCode" character varying(20) NOT NULL,
    "UserId" uuid DEFAULT '00000000-0000-0000-0000-000000000000'::uuid NOT NULL,
    "CountryId" uuid NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone,
    "City" character varying(50) DEFAULT ''::character varying NOT NULL,
    "FlatNumber" character varying(10),
    "Label" character varying(50) DEFAULT ''::character varying NOT NULL,
    "IsDefault" boolean DEFAULT false NOT NULL
);


ALTER TABLE public."ShippingAddresses" OWNER TO postgres;

--
-- TOC entry 252 (class 1259 OID 26987)
-- Name: UserPermissions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."UserPermissions" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "PermissionId" uuid NOT NULL,
    "IsGranted" boolean NOT NULL,
    "IsActive" boolean NOT NULL,
    "DateDeleted" timestamp with time zone,
    "DateCreated" timestamp with time zone NOT NULL,
    "DateEdited" timestamp with time zone
);


ALTER TABLE public."UserPermissions" OWNER TO postgres;

--
-- TOC entry 253 (class 1259 OID 26990)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 4922 (class 2604 OID 27677)
-- Name: aggregatedcounter id; Type: DEFAULT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.aggregatedcounter ALTER COLUMN id SET DEFAULT nextval('hangfire.aggregatedcounter_id_seq'::regclass);


--
-- TOC entry 4905 (class 2604 OID 27515)
-- Name: counter id; Type: DEFAULT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.counter ALTER COLUMN id SET DEFAULT nextval('hangfire.counter_id_seq'::regclass);


--
-- TOC entry 4906 (class 2604 OID 27524)
-- Name: hash id; Type: DEFAULT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.hash ALTER COLUMN id SET DEFAULT nextval('hangfire.hash_id_seq'::regclass);


--
-- TOC entry 4908 (class 2604 OID 27534)
-- Name: job id; Type: DEFAULT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.job ALTER COLUMN id SET DEFAULT nextval('hangfire.job_id_seq'::regclass);


--
-- TOC entry 4919 (class 2604 OID 27584)
-- Name: jobparameter id; Type: DEFAULT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.jobparameter ALTER COLUMN id SET DEFAULT nextval('hangfire.jobparameter_id_seq'::regclass);


--
-- TOC entry 4912 (class 2604 OID 27607)
-- Name: jobqueue id; Type: DEFAULT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.jobqueue ALTER COLUMN id SET DEFAULT nextval('hangfire.jobqueue_id_seq'::regclass);


--
-- TOC entry 4914 (class 2604 OID 27627)
-- Name: list id; Type: DEFAULT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.list ALTER COLUMN id SET DEFAULT nextval('hangfire.list_id_seq'::regclass);


--
-- TOC entry 4917 (class 2604 OID 27636)
-- Name: set id; Type: DEFAULT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.set ALTER COLUMN id SET DEFAULT nextval('hangfire.set_id_seq'::regclass);


--
-- TOC entry 4910 (class 2604 OID 27561)
-- Name: state id; Type: DEFAULT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.state ALTER COLUMN id SET DEFAULT nextval('hangfire.state_id_seq'::regclass);


--
-- TOC entry 5310 (class 0 OID 27674)
-- Dependencies: 275
-- Data for Name: aggregatedcounter; Type: TABLE DATA; Schema: hangfire; Owner: postgres
--

COPY hangfire.aggregatedcounter (id, key, value, expireat) FROM stdin;
3	stats:succeeded:2026-02-07	1	2026-03-07 18:11:06.342175+01
4	stats:succeeded:2026-02-08	1	2026-03-08 16:39:13.727764+01
8	stats:succeeded:2026-02-09	1	2026-03-09 12:20:49.022412+01
10	stats:succeeded:2026-02-10	1	2026-03-10 10:45:48.661426+01
14	stats:succeeded:2026-02-11	1	2026-03-11 13:13:33.650319+01
16	stats:succeeded:2026-02-12	1	2026-03-12 01:00:07.822737+01
19	stats:succeeded:2026-02-13	1	2026-03-13 01:25:32.930068+01
22	stats:succeeded:2026-02-14	1	2026-03-14 01:18:55.396376+01
26	stats:succeeded:2026-02-15	1	2026-03-15 17:16:02.459175+01
28	stats:succeeded:2026-02-16	1	2026-03-16 01:09:11.578906+01
32	stats:succeeded:2026-02-17	1	2026-03-17 10:49:47.972042+01
35	stats:succeeded:2026-02-18	1	2026-03-18 01:00:06.667277+01
38	stats:succeeded:2026-02-19	1	2026-03-19 01:00:09.357788+01
40	stats:succeeded:2026-02-20	1	2026-03-20 15:34:41.212113+01
43	stats:succeeded:2026-02-21-11	1	2026-02-22 12:16:37.279638+01
44	stats:succeeded:2026-02-21	1	2026-03-21 12:16:36.279638+01
2	stats:succeeded	15	\N
\.


--
-- TOC entry 5292 (class 0 OID 27389)
-- Dependencies: 257
-- Data for Name: counter; Type: TABLE DATA; Schema: hangfire; Owner: postgres
--

COPY hangfire.counter (id, key, value, expireat) FROM stdin;
\.


--
-- TOC entry 5294 (class 0 OID 27397)
-- Dependencies: 259
-- Data for Name: hash; Type: TABLE DATA; Schema: hangfire; Owner: postgres
--

COPY hangfire.hash (id, key, field, value, expireat, updatecount) FROM stdin;
1	recurring-job:update-rental-statuses	Queue	default	\N	0
2	recurring-job:update-rental-statuses	Cron	0 0 * * *	\N	0
3	recurring-job:update-rental-statuses	TimeZoneId	UTC	\N	0
4	recurring-job:update-rental-statuses	Job	{"Type":"ByteBuy.Infrastructure.HangfireJobs.RentalStatusJob, ByteBuy.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null","Method":"Execute","ParameterTypes":"[]","Arguments":"[]"}	\N	0
5	recurring-job:update-rental-statuses	CreatedAt	2026-02-07T17:10:15.4662085Z	\N	0
7	recurring-job:update-rental-statuses	V	2	\N	0
8	recurring-job:update-rental-statuses	LastExecution	2026-02-21T11:16:35.1643528Z	\N	0
6	recurring-job:update-rental-statuses	NextExecution	2026-02-22T00:00:00.0000000Z	\N	0
9	recurring-job:update-rental-statuses	LastJobId	15	\N	0
\.


--
-- TOC entry 5296 (class 0 OID 27408)
-- Dependencies: 261
-- Data for Name: job; Type: TABLE DATA; Schema: hangfire; Owner: postgres
--

COPY hangfire.job (id, stateid, statename, invocationdata, arguments, createdat, expireat, updatecount) FROM stdin;
15	45	Succeeded	{"Type": "ByteBuy.Infrastructure.HangfireJobs.RentalStatusJob, ByteBuy.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Method": "Execute", "Arguments": "[]", "ParameterTypes": "[]"}	[]	2026-02-21 12:16:35.216666+01	2026-02-22 12:16:37.279638+01	0
\.


--
-- TOC entry 5307 (class 0 OID 27468)
-- Dependencies: 272
-- Data for Name: jobparameter; Type: TABLE DATA; Schema: hangfire; Owner: postgres
--

COPY hangfire.jobparameter (id, jobid, name, value, updatecount) FROM stdin;
57	15	RecurringJobId	"update-rental-statuses"	0
58	15	Time	1771672595	0
59	15	CurrentCulture	"en-US"	0
60	15	CurrentUICulture	"en-US"	0
\.


--
-- TOC entry 5300 (class 0 OID 27433)
-- Dependencies: 265
-- Data for Name: jobqueue; Type: TABLE DATA; Schema: hangfire; Owner: postgres
--

COPY hangfire.jobqueue (id, jobid, queue, fetchedat, updatecount) FROM stdin;
\.


--
-- TOC entry 5302 (class 0 OID 27441)
-- Dependencies: 267
-- Data for Name: list; Type: TABLE DATA; Schema: hangfire; Owner: postgres
--

COPY hangfire.list (id, key, value, expireat, updatecount) FROM stdin;
\.


--
-- TOC entry 5308 (class 0 OID 27482)
-- Dependencies: 273
-- Data for Name: lock; Type: TABLE DATA; Schema: hangfire; Owner: postgres
--

COPY hangfire.lock (resource, updatecount, acquired) FROM stdin;
\.


--
-- TOC entry 5290 (class 0 OID 27383)
-- Dependencies: 255
-- Data for Name: schema; Type: TABLE DATA; Schema: hangfire; Owner: postgres
--

COPY hangfire.schema (version) FROM stdin;
23
\.


--
-- TOC entry 5303 (class 0 OID 27449)
-- Dependencies: 268
-- Data for Name: server; Type: TABLE DATA; Schema: hangfire; Owner: postgres
--

COPY hangfire.server (id, data, lastheartbeat, updatecount) FROM stdin;
wojciechpc:18872:e30868c6-3c21-47db-ae0d-620fbea4f950	{"Queues": ["default"], "StartedAt": "2026-02-21T15:40:07.6484131Z", "WorkerCount": 20}	2026-02-21 17:18:39.28146+01	0
\.


--
-- TOC entry 5305 (class 0 OID 27457)
-- Dependencies: 270
-- Data for Name: set; Type: TABLE DATA; Schema: hangfire; Owner: postgres
--

COPY hangfire.set (id, key, score, value, expireat, updatecount) FROM stdin;
1	recurring-jobs	1771718400	update-rental-statuses	\N	0
\.


--
-- TOC entry 5298 (class 0 OID 27418)
-- Dependencies: 263
-- Data for Name: state; Type: TABLE DATA; Schema: hangfire; Owner: postgres
--

COPY hangfire.state (id, jobid, name, reason, createdat, data, updatecount) FROM stdin;
43	15	Enqueued	Triggered by recurring job scheduler	2026-02-21 12:16:35.278886+01	{"Queue": "default", "EnqueuedAt": "2026-02-21T11:16:35.2722280Z"}	0
44	15	Processing	\N	2026-02-21 12:16:35.304923+01	{"ServerId": "wojciechpc:10808:c13fba38-b816-4fec-96ed-4d6bc498658c", "WorkerId": "8d5ac420-d17b-4d2c-adec-003bbea83ef4", "StartedAt": "2026-02-21T11:16:35.2967172Z"}	0
45	15	Succeeded	\N	2026-02-21 12:16:37.281486+01	{"Latency": "90", "SucceededAt": "2026-02-21T11:16:37.2698128Z", "PerformanceDuration": "1961"}	0
\.


--
-- TOC entry 5253 (class 0 OID 26820)
-- Dependencies: 218
-- Data for Name: AspNetRoleClaims; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetRoleClaims" ("Id", "RoleId", "ClaimType", "ClaimValue") FROM stdin;
\.


--
-- TOC entry 5255 (class 0 OID 26826)
-- Dependencies: 220
-- Data for Name: AspNetRoles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetRoles" ("Id", "IsActive", "DateCreated", "DateEdited", "DateDeleted", "Name", "NormalizedName", "ConcurrencyStamp", "IsSystemRole") FROM stdin;
019a6de8-1bbf-7715-9eb1-0f800e19395a	t	2025-11-10 14:15:19.340421+01	2026-02-19 19:22:29.23328+01	\N	Portal User	PORTAL USER	99fbfc95-0515-4927-b18d-4d28201b43e6	f
019c6727-f863-7e7f-8a49-14d79265d614	t	2026-02-16 16:53:18.686883+01	2026-02-21 17:10:34.824145+01	\N	Store Keeper	STORE KEEPER	553afbda-3b57-43c1-9851-5316f142eba0	f
019b0e2f-9047-748c-b1d1-13fff048abcd	t	2025-12-11 17:12:36.803544+01	2026-02-21 17:13:55.676155+01	\N	Storage Worker	STORAGE WORKER	abfaba78-2a12-45b1-9865-6433b3580405	f
019c49b1-4170-779e-85d2-6a2e26582981	t	2026-02-10 23:34:39.340406+01	2026-02-16 13:15:18.992779+01	\N	Owner	OWNER	9cef7331-5146-48ed-aeab-0d7425b695c2	t
019c66a1-2e93-7aba-8015-968c6a165159	t	2026-02-16 14:26:05.191903+01	2026-02-16 15:17:31.527799+01	\N	Admin	ADMIN	b2778337-8e1e-41d9-ae34-9a660ad34d28	f
\.


--
-- TOC entry 5256 (class 0 OID 26831)
-- Dependencies: 221
-- Data for Name: AspNetUserClaims; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetUserClaims" ("Id", "UserId", "ClaimType", "ClaimValue") FROM stdin;
\.


--
-- TOC entry 5258 (class 0 OID 26837)
-- Dependencies: 223
-- Data for Name: AspNetUserLogins; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetUserLogins" ("LoginProvider", "ProviderKey", "ProviderDisplayName", "UserId") FROM stdin;
\.


--
-- TOC entry 5259 (class 0 OID 26842)
-- Dependencies: 224
-- Data for Name: AspNetUserRoles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetUserRoles" ("UserId", "RoleId") FROM stdin;
fdcbf4ff-59d7-4622-b586-9c1e298fc1dc	019c66a1-2e93-7aba-8015-968c6a165159
7ae34c5a-51fe-45c2-9050-d1ee79eed55d	019b0e2f-9047-748c-b1d1-13fff048abcd
280fa9bd-1d64-4f59-bb91-c6a539cdefba	019a6de8-1bbf-7715-9eb1-0f800e19395a
bd3395c3-83c0-4fd4-9d0a-42b4ab6320fc	019a6de8-1bbf-7715-9eb1-0f800e19395a
90e966fd-ed39-4e59-be97-fdff12504616	019c6727-f863-7e7f-8a49-14d79265d614
bea1f25c-46f5-4e94-9bed-9697d6dd078c	019c49b1-4170-779e-85d2-6a2e26582981
\.


--
-- TOC entry 5260 (class 0 OID 26845)
-- Dependencies: 225
-- Data for Name: AspNetUserTokens; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetUserTokens" ("UserId", "LoginProvider", "Name", "Value") FROM stdin;
\.


--
-- TOC entry 5261 (class 0 OID 26850)
-- Dependencies: 226
-- Data for Name: AspNetUsers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetUsers" ("Id", "FirstName", "LastName", "IsActive", "DateCreated", "DateEdited", "DateDeleted", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount", "HomeAddress_City", "HomeAddress_Country", "HomeAddress_FlatNumber", "HomeAddress_HouseNumber", "HomeAddress_PostalCode", "HomeAddress_Street", "UserType", "HomeAddress_PostalCity", "CartId", "CompanyId") FROM stdin;
90e966fd-ed39-4e59-be97-fdff12504616	Jolanta	Baziak	t	2026-02-21 17:02:14.168005+01	\N	\N	jolantab@gmail.com	JOLANTAB@GMAIL.COM	jolantab@gmail.com	JOLANTAB@GMAIL.COM	f	AQAAAAIAAYagAAAAEDIZkhhqPKRekGTucJ5itdD34rVxwXJtrsxUWf/AAGXbUVdjkit3jtYqtMaBL3X8sw==	UBUJ3FMVMYE6SMW2CXDRLJUQ7WWKXN7R	ee98c945-1114-46f7-8bd6-de85e0812d3a	897123123	f	f	\N	t	0	Korzenna	Poland		67	33-322	Korzenna	Employee	Korzenna	\N	effc9ac8-1c0b-4bd5-8c88-ff59eac28852
7ae34c5a-51fe-45c2-9050-d1ee79eed55d	Zbigniew	Niezgoda	t	2026-02-21 16:48:51.400555+01	2026-02-21 17:11:52.950194+01	\N	zbygniewn@gmail.com	ZBYGNIEWN@GMAIL.COM	zbygniewn@gmail.com	ZBYGNIEWN@GMAIL.COM	f	AQAAAAIAAYagAAAAELraG4Ilg2nJ5mKfLBzaLkV8Cxy2czcKzaJ231OxGfxNlBoLrZWui6hUho+ZVnjrKw==	SRMUWEM2OVH5CF43VJIUXJAPEK64SITJ	7b58bcdd-bb58-448f-b669-c8a866d36f22	761234123	f	f	\N	t	0	Siedlce	Poland		89	33-322	Siedlce	Employee	Korzenna	\N	effc9ac8-1c0b-4bd5-8c88-ff59eac28852
280fa9bd-1d64-4f59-bb91-c6a539cdefba	Joanna	Nowak	t	2026-02-21 16:50:37.659951+01	2026-02-21 16:50:37.660064+01	\N	joanna12@gmail.com	JOANNA12@GMAIL.COM	joanna12@gmail.com	JOANNA12@GMAIL.COM	f	AQAAAAIAAYagAAAAEMbsy3FJiMSBF5mA3oxyAeMW+4Tk2+j4ZxlMWA9XHgu8s0EBTf7GHFa5TJjaAZ8JRQ==	IWG6FA7SAB2WKGMUVGY2XJHSXGMIQSKK	152103a0-8464-4af1-b2b9-704504e20d3c	724123789	f	f	\N	t	0	Sloneczna	Poland	2	67	33-666	Krakow	PortalUser	Kraków	14f6f790-2c4b-47db-82a4-3bf483925141	\N
fdcbf4ff-59d7-4622-b586-9c1e298fc1dc	Jan	Nowak	t	2026-02-21 16:43:51.275805+01	\N	\N	jannowak12@gmail.com	JANNOWAK12@GMAIL.COM	jannowak12@gmail.com	JANNOWAK12@GMAIL.COM	f	AQAAAAIAAYagAAAAENIIsrsvxJrlLU1ZkniNexH4TnyJyYanNRp8/NWTWWegS/Ts7i/W3IZYC7UNx/EQhw==	WURMMM7QH4KV34DRCNMJC4DY5EEPXJ32	4ee2c586-ed3a-4f21-aa97-17bfac22cd90	724671234	f	f	\N	t	0	Nowy Sącz	Poland	2	56	33-300	Lwowska	Employee	Nowy Sącz	\N	effc9ac8-1c0b-4bd5-8c88-ff59eac28852
bd3395c3-83c0-4fd4-9d0a-42b4ab6320fc	Adam	Pazdan	t	2026-02-21 16:52:14.307495+01	2026-02-21 16:52:14.307499+01	\N	adamp123@gmail.com	ADAMP123@GMAIL.COM	adamp123@gmail.com	ADAMP123@GMAIL.COM	f	AQAAAAIAAYagAAAAEBM5ALwev83so9DJAO+FQdp2LAMTGZvOo1UjDp2KEFBs2EQxZ7cChcMr22hihBW31Q==	HXQ7HG4R3GZ5OICF3WRNTEODETLZH62Z	865040cd-bad9-4096-ba50-ad02ef151886	897123456	f	f	\N	t	0	Siedlce	Poland		89	33-322	Siedlce	PortalUser	Korzenna	12763c7c-ed1f-43a1-ac9b-16108c9d4985	\N
bea1f25c-46f5-4e94-9bed-9697d6dd078c	Wojciech	Mucha	t	2026-02-10 23:37:25.782836+01	2026-02-21 17:13:29.8994+01	\N	wojciechmucha12@gmail.com	WOJCIECHMUCHA12@GMAIL.COM	wojciechmucha12@gmail.com	WOJCIECHMUCHA12@GMAIL.COM	f	AQAAAAIAAYagAAAAEJzFgBh9edxyoDMGdmJ/pLeJY0mKqo2g7K0Cmc0VDnTrezbIipOyLJPuCDfcVIMC6g==	N2AYI6TWVPJETC3KR6TLHV3O3PMIJTVR	409c378f-2131-4082-a374-dbfa35a8ff7d	678123123	f	f	\N	t	0	Siedlce	Poland	8	78	33-322	Siedlce	Employee	Korzenna	\N	effc9ac8-1c0b-4bd5-8c88-ff59eac28852
\.


--
-- TOC entry 5262 (class 0 OID 26856)
-- Dependencies: 227
-- Data for Name: CartOffers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."CartOffers" ("Id", "Quantity", "CartId", "OfferId", "IsActive", "DateDeleted", "DateCreated", "DateEdited") FROM stdin;
\.


--
-- TOC entry 5263 (class 0 OID 26859)
-- Dependencies: 228
-- Data for Name: Carts; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Carts" ("Id", "UserId", "TotalCartValue_Amount", "TotalCartValue_Currency", "TotalItemsValue_Amount", "TotalItemsValue_Currency", "IsActive", "DateDeleted", "DateCreated", "DateEdited") FROM stdin;
14f6f790-2c4b-47db-82a4-3bf483925141	280fa9bd-1d64-4f59-bb91-c6a539cdefba	0.000	PLN	0.000	PLN	t	\N	2026-02-21 16:50:37.66055+01	\N
12763c7c-ed1f-43a1-ac9b-16108c9d4985	bd3395c3-83c0-4fd4-9d0a-42b4ab6320fc	0.000	PLN	0.000	PLN	t	\N	2026-02-21 16:52:14.307505+01	\N
\.


--
-- TOC entry 5264 (class 0 OID 26862)
-- Dependencies: 229
-- Data for Name: Categories; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Categories" ("Id", "Name", "Description", "IsActive", "DateDeleted", "DateCreated", "DateEdited") FROM stdin;
019b2da4-675f-782d-966f-11d40a674446	GPU	Graphics Card	t	\N	2025-12-17 19:48:27.743098+01	\N
019b08b1-c552-7902-88a9-af472ee85fa8	CPU	Central Processing Unit	t	\N	2025-12-10 15:37:06.770038+01	2025-12-17 19:48:35.635469+01
019b2da3-f0a5-7957-a54a-68c9a4d9137a	Fan	Fans of all kind,  not only fans	t	\N	2025-12-17 19:47:57.347706+01	2025-12-17 19:48:38.070185+01
019b08b1-a7a4-726d-be07-e74a0d156917	RAM	Random Access Memory	t	\N	2025-12-10 15:36:59.16424+01	2025-12-17 19:48:42.434742+01
019b2da4-db1f-7ba6-bd24-c8970570ee63	PC Case	All kinds of pc cases	t	\N	2025-12-17 19:48:57.375551+01	2025-12-17 19:49:02.177599+01
019b2da5-787f-7005-aedb-e987795db959	PSU	Power supply units of all kind	t	\N	2025-12-17 19:49:37.663359+01	2025-12-17 19:49:43.643427+01
019c32e0-d6f7-7d1a-8c19-48415763889d	PC	Computer	t	\N	2026-02-06 13:15:21.838533+01	2026-02-16 15:55:39.689431+01
019be16f-4cdd-71b4-90c1-d464357bdcfd	HDMI Cable	Cable to connect display and your device !	t	\N	2026-01-21 17:42:06.428204+01	2026-02-20 22:29:13.373265+01
019c7cf5-9329-7fac-8289-87906714703a	Test	Test	f	2026-02-20 22:29:59.592851+01	2026-02-20 22:29:54.729536+01	\N
019c800e-9037-7f9a-b87a-18fac894a09d	Water Colling System	Test	f	2026-02-21 12:56:06.480871+01	2026-02-21 12:56:04.022726+01	\N
019c8036-d217-7aa3-b067-b72e0d51882e	Water Cooling TEst	Test	f	2026-02-21 13:40:13.56971+01	2026-02-21 13:40:02.326812+01	2026-02-21 13:40:11.175611+01
\.


--
-- TOC entry 5265 (class 0 OID 26865)
-- Dependencies: 230
-- Data for Name: Company; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Company" ("Id", "CompanyName", "TIN", "CompanyAddress_Street", "CompanyAddress_HouseNumber", "CompanyAddress_PostalCode", "CompanyAddress_PostalCity", "CompanyAddress_City", "CompanyAddress_Country", "CompanyAddress_FlatNumber", "Email", "Phone", "Slogan", "DateCreated", "DateEdited") FROM stdin;
effc9ac8-1c0b-4bd5-8c88-ff59eac28852	Byte&Buy	22345678978	Lwowska	41	33-300	Nowy Sącz	Nowy Sacz	Poland	12	bytebuy@gmail.com	689123123	BuyIT.RentIT.ByteIT	2026-12-12 00:00:00+01	2026-02-21 16:15:40.764662+01
\.


--
-- TOC entry 5266 (class 0 OID 26870)
-- Dependencies: 231
-- Data for Name: Conditions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Conditions" ("Id", "Name", "Description", "IsActive", "DateDeleted", "DateCreated", "DateEdited") FROM stdin;
019b086f-ce21-7ce3-8908-a2f43d6cae95	Used	In good condition with traces of use	t	\N	2025-12-10 14:25:03.648809+01	2025-12-11 17:09:38.218299+01
019a9c3e-db6a-7626-b395-1777c739456f	New	Item has never been used	t	\N	2025-11-19 14:12:36.457247+01	2025-12-11 17:26:33.800852+01
019b08f3-b748-70d7-b30f-460522c46935	Very Good	Condition almost like new	t	\N	2025-12-10 16:49:08.552312+01	2026-02-16 01:22:00.413575+01
019c66f3-94dc-793e-b4c7-50a9e46f14f0	Outlet	Outlet	f	2026-02-20 22:30:02.047029+01	2026-02-16 15:56:05.339118+01	2026-02-20 22:29:47.019443+01
019c800e-f266-7ea7-9d26-998f7bc19120	Outlet	test	f	2026-02-21 12:56:38.095821+01	2026-02-21 12:56:29.157099+01	\N
019c8037-338c-770e-95c1-ed24ba657256	Test	Test	f	2026-02-21 13:40:38.192618+01	2026-02-21 13:40:27.275365+01	\N
\.


--
-- TOC entry 5267 (class 0 OID 26873)
-- Dependencies: 232
-- Data for Name: Countries; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Countries" ("Id", "Name", "Code", "IsActive", "DateDeleted", "DateCreated", "DateEdited") FROM stdin;
019aac16-4916-74ed-ba2f-3be44a87ac0f	Poland	POL	t	\N	2025-11-22 16:02:13.013235+01	2025-12-12 00:14:57.689487+01
\.


--
-- TOC entry 5268 (class 0 OID 26876)
-- Dependencies: 233
-- Data for Name: Deliveries; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Deliveries" ("Id", "Name", "Description", "Price_Amount", "Price_Currency", "IsActive", "DateDeleted", "DateCreated", "DateEdited", "Channel", "ParcelSize", "DeliveryCarrierId") FROM stdin;
019bb83f-9664-76e9-946d-d71c18f9ffab	Żabka Pickup	\N	10.000	PLN	t	\N	2026-01-13 17:45:33.667092+01	\N	PickupPoint	\N	019b504c-6f54-74a5-9e3d-39c86e65cdb6
019b504c-ecdc-7447-a2e0-89f877b47d3b	Inpost Courier	\N	25.000	PLN	t	\N	2025-12-24 13:19:37.29694+01	2026-01-14 12:35:37.56151+01	Courier	\N	019b504c-6f54-74a5-9e3d-39c86e65cdb6
019bebd8-71ff-7450-bf3e-f1a2cb5b6c08	FedEx Parcel Locker - S	\N	19.990	PLN	t	\N	2026-01-23 18:13:09.37525+01	2026-02-16 01:21:38.584137+01	ParcelLocker	Small	019bb3bc-3175-773e-a281-a35331eb8fb7
019c66f1-c1e1-7424-8457-c0c85e50bbad	Inpost Parcel Locker - L	\N	25.000	PLN	t	\N	2026-02-16 15:54:05.789248+01	\N	ParcelLocker	Large	019b504c-6f54-74a5-9e3d-39c86e65cdb6
019bebd8-c71f-782b-8699-a2e71f54311a	FedEx Parcel Locker - M	\N	23.000	PLN	t	\N	2026-01-23 18:13:31.167507+01	2026-02-16 15:54:11.976725+01	ParcelLocker	Medium	019bb3bc-3175-773e-a281-a35331eb8fb7
019bb83c-738c-77f7-972c-122a8eb37195	Inpost Parcel Locker - M	\N	25.000	PLN	t	\N	2026-01-13 17:42:08.123879+01	2026-02-16 15:54:24.594566+01	ParcelLocker	Medium	019b504c-6f54-74a5-9e3d-39c86e65cdb6
019c7091-37e2-780b-821f-33ba1efc0286	Inpost Parcel Locker - S	For cpus, ram	17.990	PLN	t	\N	2026-02-18 12:44:51.17045+01	\N	ParcelLocker	Small	019b504c-6f54-74a5-9e3d-39c86e65cdb6
019c8036-3652-7800-b41a-b748d77a590c	Pocztex Courier	Test	25.000	PLN	t	\N	2026-02-21 13:39:22.448379+01	2026-02-21 13:39:30.579714+01	Courier	\N	019c800d-5e1c-74ab-b49c-6e931a9519c2
\.


--
-- TOC entry 5269 (class 0 OID 26883)
-- Dependencies: 234
-- Data for Name: DeliveryCarriers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."DeliveryCarriers" ("Id", "Name", "Code", "IsActive", "DateDeleted", "DateCreated", "DateEdited") FROM stdin;
019bb3bc-3175-773e-a281-a35331eb8fb7	FedEx	FDX	t	\N	2026-01-12 20:43:33.745791+01	\N
019b504c-6f54-74a5-9e3d-39c86e65cdb6	Inpost	INPOST	t	\N	2025-12-24 13:19:05.145016+01	2026-01-31 13:44:56.422673+01
019c800d-5e1c-74ab-b49c-6e931a9519c2	Pocztex	PCT	t	\N	2026-02-21 12:54:45.656228+01	2026-02-21 13:38:40.969825+01
\.


--
-- TOC entry 5270 (class 0 OID 26886)
-- Dependencies: 235
-- Data for Name: Images; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Images" ("Id", "ImagePath", "ItemId", "AltText", "IsActive", "DateDeleted", "DateCreated", "DateEdited") FROM stdin;
019c80ec-70d9-7d01-970a-c5522453a708	Items/12220514-020c-4875-b097-1c42a588da3c.jpg	e4ea24bd-eccf-484f-86f6-6f6c79332235	1	t	\N	2026-02-21 16:58:24.96848+01	\N
019c80ec-70e3-72cb-b92a-f5f7810ffbe7	Items/f9f8dd86-7b36-478e-b4f0-724f9eacd249.jpg	e4ea24bd-eccf-484f-86f6-6f6c79332235	6	t	\N	2026-02-21 16:58:24.968634+01	\N
019c80ec-70e3-7427-b762-c47ed2b226c3	Items/bf8bbb0e-705e-46e5-9a16-5e9993e1c9d2.jpg	e4ea24bd-eccf-484f-86f6-6f6c79332235	5	t	\N	2026-02-21 16:58:24.968634+01	\N
019c80ec-70e3-74ff-b6c6-e957116e0831	Items/31072898-5252-4ae3-a114-db52023cb214.jpg	e4ea24bd-eccf-484f-86f6-6f6c79332235	2	t	\N	2026-02-21 16:58:24.968632+01	\N
019c80ec-70e3-7775-90d7-4a940be52e4f	Items/3ff3e402-4bc0-459a-84b6-d81e885ec7c4.jpg	e4ea24bd-eccf-484f-86f6-6f6c79332235	3	t	\N	2026-02-21 16:58:24.968633+01	\N
019c80ec-70e3-7af6-a301-68cd1f4fe71a	Items/c1b3ff70-d855-443c-b887-68c80109eaff.jpg	e4ea24bd-eccf-484f-86f6-6f6c79332235	4	t	\N	2026-02-21 16:58:24.968633+01	\N
019c80ed-64d3-70e5-9948-47b8193cfb44	Items/553a7200-e8a8-4c9a-b382-b8bf6b28bd04.jpg	14f043bb-9264-4c97-97ba-23db8e5d5624	1	t	\N	2026-02-21 16:59:27.443225+01	\N
019c80ed-64d3-7c2b-ba7b-ace79fdd20c5	Items/84c62b05-bdc7-4bee-86e5-e6dc7267d54a.jpg	14f043bb-9264-4c97-97ba-23db8e5d5624	3Rt	t	\N	2026-02-21 16:59:27.443232+01	\N
019c80ed-64d3-7f6a-bfc6-3647c2f2455c	Items/529bc54d-5894-4fc4-85cf-e10a24273fba.jpg	14f043bb-9264-4c97-97ba-23db8e5d5624	2	t	\N	2026-02-21 16:59:27.443226+01	\N
019c80fc-9d42-7373-878e-ef8eea293977	Items/9e275629-e99a-41d4-8350-37d9c6da3c58.PNG	1071ea1b-d757-44db-8122-8533e092fdad	3	t	\N	2026-02-21 17:16:04.927242+01	\N
019c80fc-9d42-75d1-88a6-1e8760790349	Items/407cfcd8-79e8-4a6a-ba12-c4a5c87c4ff7.PNG	1071ea1b-d757-44db-8122-8533e092fdad	1	t	\N	2026-02-21 17:16:04.927241+01	\N
019c80fc-9d42-7a20-b2ef-f812158ea9b2	Items/630eecef-30b9-4690-8472-39d548c37fdf.jpg	1071ea1b-d757-44db-8122-8533e092fdad	4	t	\N	2026-02-21 17:16:04.927244+01	\N
019c80fc-9d42-7b27-9e2f-68861a518160	Items/597a266d-83ad-47ef-8f82-c1e087d395ae.jpg	1071ea1b-d757-44db-8122-8533e092fdad	2	t	\N	2026-02-21 17:16:04.927242+01	\N
019c80fc-9d42-7b56-b16d-41891de09a61	Items/ed37a329-435f-4f33-8745-c25d6a8ac0d6.jpg	1071ea1b-d757-44db-8122-8533e092fdad	3	t	\N	2026-02-21 17:16:04.927244+01	\N
019c80fc-9d42-7df2-b063-5889a21deed2	Items/60338c23-7ed5-4181-b477-17b459c35ebb.jpg	1071ea1b-d757-44db-8122-8533e092fdad	5	t	\N	2026-02-21 17:16:04.927244+01	\N
019c80fe-2839-715e-86f7-08a40b9e9b0a	Items/c9c171c0-4ebc-49bc-8841-8d549c4e855f.jpg	b1366a2e-1296-479a-8d06-bcd2b38a85d8	6	t	\N	2026-02-21 17:17:46.041566+01	\N
019c80fe-2839-7459-a056-3572fc5fe4a4	Items/143b4ae0-36d2-4fbf-a720-ef5eae4b4bd4.jpg	b1366a2e-1296-479a-8d06-bcd2b38a85d8	4	t	\N	2026-02-21 17:17:46.041566+01	\N
019c80fe-2839-75ca-831f-001984dc8a2c	Items/02412f8e-9e2e-411b-9c5a-be150a45d257.PNG	b1366a2e-1296-479a-8d06-bcd2b38a85d8	3	t	\N	2026-02-21 17:17:46.041565+01	\N
019c80fe-2839-7883-9118-238aad257a3e	Items/b1afbfc4-e4f4-4255-9a90-542bda7e4b15.PNG	b1366a2e-1296-479a-8d06-bcd2b38a85d8	1	t	\N	2026-02-21 17:17:46.041564+01	\N
019c80fe-2839-7999-866c-ffe70843d9b8	Items/bad7a661-0b55-4bcb-aa54-f0c487b6f47f.jpg	b1366a2e-1296-479a-8d06-bcd2b38a85d8	5	t	\N	2026-02-21 17:17:46.041566+01	\N
019c80fe-2839-79a8-a250-490b9163e387	Items/0f7f539c-51c8-420d-8d2e-9bf7bb347d3a.PNG	b1366a2e-1296-479a-8d06-bcd2b38a85d8	2	t	\N	2026-02-21 17:17:46.041565+01	\N
\.


--
-- TOC entry 5271 (class 0 OID 26891)
-- Dependencies: 236
-- Data for Name: Items; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Items" ("Id", "Name", "Description", "CategoryId", "ConditionId", "StockQuantity", "IsActive", "DateDeleted", "DateCreated", "DateEdited", "IsCompanyItem") FROM stdin;
aacce065-e9b0-4b97-836a-f4468b60169d	Ryzne Test Cpu to show it works	Test2	019b2da3-f0a5-7957-a54a-68c9a4d9137a	019a9c3e-db6a-7626-b395-1777c739456f	0	t	\N	2026-02-21 13:56:18.230506+01	\N	f
6d10ff90-32c2-4571-8dbd-ee5b9b06bb75	Rental offer to show how works	Test	019b08b1-c552-7902-88a9-af472ee85fa8	019a9c3e-db6a-7626-b395-1777c739456f	0	t	\N	2026-02-21 13:58:04.413441+01	\N	f
37fdb3cd-77c8-44ee-944f-93f1a0d42577	Intel Core i5 4670K Rental	Cpu for rental, fully operating	019b08b1-c552-7902-88a9-af472ee85fa8	019b086f-ce21-7ce3-8908-a2f43d6cae95	0	t	\N	2026-02-18 13:14:34.659687+01	\N	f
f0300ec2-58b1-46e5-b20a-b787b78631ed	TestTestTestTestTest	Test	019b2da4-675f-782d-966f-11d40a674446	019b08f3-b748-70d7-b30f-460522c46935	12	f	2026-02-16 16:55:32.816637+01	2026-02-16 16:21:51.330771+01	\N	t
98e7e5bf-f384-42de-87fb-72e4f0b40082	Gigabyte RX 580 4GB	Test	019b2da4-675f-782d-966f-11d40a674446	019b086f-ce21-7ce3-8908-a2f43d6cae95	50	f	2026-02-21 16:14:15.644554+01	2026-02-21 13:42:21.412762+01	\N	t
ff2d1655-3eac-4137-a62d-5eb03a1b1bf3	Nvidia Gtx 1080 TI	Test	019b2da4-675f-782d-966f-11d40a674446	019b086f-ce21-7ce3-8908-a2f43d6cae95	0	t	\N	2026-02-20 22:54:45.52057+01	\N	f
0b4f606e-670b-40c6-8cae-b7919ef3a7e7	Testasdasdasdasdasd	Test	019b2da3-f0a5-7957-a54a-68c9a4d9137a	019b08f3-b748-70d7-b30f-460522c46935	12	f	2026-02-16 23:19:14.581733+01	2026-02-16 16:57:13.15126+01	\N	t
4b62c742-6127-4f1d-8260-d37e2b149859	Nvidia Gtx 1080 TI	Test	019b2da4-675f-782d-966f-11d40a674446	019b086f-ce21-7ce3-8908-a2f43d6cae95	0	t	\N	2026-02-20 22:54:45.783567+01	\N	f
5a62187c-0608-4d10-8da4-2e5ddcb75743	Ryzen 5 5600 3.9GHZ	Just a test description	019b08b1-c552-7902-88a9-af472ee85fa8	019b086f-ce21-7ce3-8908-a2f43d6cae95	6	f	2026-02-21 16:14:16.831838+01	2026-02-21 12:58:03.882468+01	\N	t
83373a58-0686-4199-b15c-d98e404b9980	Gtx 1080 TI Founders Edition	This gpu is a legend.	019b2da4-675f-782d-966f-11d40a674446	019b086f-ce21-7ce3-8908-a2f43d6cae95	0	f	2026-02-21 16:14:18.086727+01	2026-02-16 16:08:35.375683+01	\N	t
be406a69-0819-45ca-8fd6-9f4038143f62	Ryzen 5 5600 3.9 GHZ	Used Cpu, works very well 	019b08b1-c552-7902-88a9-af472ee85fa8	019b086f-ce21-7ce3-8908-a2f43d6cae95	91	f	2026-02-21 16:20:29.553413+01	2026-02-16 16:05:54.471452+01	\N	t
e4ea24bd-eccf-484f-86f6-6f6c79332235	Palit RTX 3070 8GB	Palit RTX 3070 8GB fully working	019b2da4-675f-782d-966f-11d40a674446	019b086f-ce21-7ce3-8908-a2f43d6cae95	2	t	\N	2026-02-21 16:58:24.967913+01	\N	t
70278a7e-07c4-4d05-bc34-ae80eec48660	joannanowak@gmail.com	test	019b2da4-675f-782d-966f-11d40a674446	019b086f-ce21-7ce3-8908-a2f43d6cae95	0	f	2026-02-18 00:48:14.427023+01	2026-02-18 00:39:56.514315+01	\N	f
5ed1f9e9-e936-44f0-ace2-aa303960a8c4	Nvidia Gtx 1080 TI	test	019b08b1-c552-7902-88a9-af472ee85fa8	019b086f-ce21-7ce3-8908-a2f43d6cae95	0	f	2026-02-18 00:48:40.335222+01	2026-02-18 00:31:34.228732+01	\N	f
ee69d9a7-805a-47ec-ade8-4420db72e9f4	this is a test method for presentation	Test	019b2da4-675f-782d-966f-11d40a674446	019b086f-ce21-7ce3-8908-a2f43d6cae95	0	f	2026-02-21 12:56:42.097248+01	2026-02-20 22:32:18.958274+01	\N	t
14f043bb-9264-4c97-97ba-23db8e5d5624	AMD Ryzen 5 5 5600	Ryzen 5 5600 Fully working used for a couple of years	019b08b1-c552-7902-88a9-af472ee85fa8	019b086f-ce21-7ce3-8908-a2f43d6cae95	20	t	\N	2026-02-21 16:59:27.443223+01	\N	t
1071ea1b-d757-44db-8122-8533e092fdad	GIGABYTE RX 580 4GB	Gpu from mining factory, all works fine	019b2da4-675f-782d-966f-11d40a674446	019b086f-ce21-7ce3-8908-a2f43d6cae95	0	t	\N	2026-02-21 17:16:04.927239+01	\N	f
b1366a2e-1296-479a-8d06-bcd2b38a85d8	PLATFORM Athlon 5350 + MOBO	"Very powerful" MOBO + CPU ideal for watching desktop	019c32e0-d6f7-7d1a-8c19-48415763889d	019b086f-ce21-7ce3-8908-a2f43d6cae95	0	t	\N	2026-02-21 17:17:46.041563+01	\N	f
3d058b60-1fd6-4286-99bb-28213c458a76	Intel Core i5 4670K 	Used, fully working tho.	019b08b1-c552-7902-88a9-af472ee85fa8	019b086f-ce21-7ce3-8908-a2f43d6cae95	0	t	\N	2026-02-18 13:12:10.757739+01	\N	f
\.


--
-- TOC entry 5272 (class 0 OID 26897)
-- Dependencies: 237
-- Data for Name: OfferDeliveries; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."OfferDeliveries" ("Id", "OfferId", "DeliveryId", "IsActive", "DateDeleted", "DateCreated", "DateEdited") FROM stdin;
019c80f3-92e5-73dd-8ffb-459e374adb95	019c80f3-92c8-7022-93e7-7034044b80e6	019bb83f-9664-76e9-946d-d71c18f9ffab	t	\N	2026-02-21 17:06:12.42171+01	\N
019c80f3-92ee-71f2-a4fa-bb0bf855ef53	019c80f3-92c8-7022-93e7-7034044b80e6	019c8036-3652-7800-b41a-b748d77a590c	t	\N	2026-02-21 17:06:12.421743+01	\N
019c80f3-92ee-7554-b52e-8467b82b3856	019c80f3-92c8-7022-93e7-7034044b80e6	019b504c-ecdc-7447-a2e0-89f877b47d3b	t	\N	2026-02-21 17:06:12.421743+01	\N
019c80f3-92ee-7e53-a396-d0d53783d629	019c80f3-92c8-7022-93e7-7034044b80e6	019c7091-37e2-780b-821f-33ba1efc0286	t	\N	2026-02-21 17:06:12.421743+01	\N
019c80f3-92ee-7fc9-a457-828f7b6acd3a	019c80f3-92c8-7022-93e7-7034044b80e6	019bebd8-71ff-7450-bf3e-f1a2cb5b6c08	t	\N	2026-02-21 17:06:12.421743+01	\N
019c80f3-d115-70cd-9bf7-4a56e188a79b	019c80f3-d111-7deb-a77a-d95cd7b0fad5	019bebd8-71ff-7450-bf3e-f1a2cb5b6c08	t	\N	2026-02-21 17:06:28.369232+01	\N
019c80f3-d115-767c-8435-8744f8664a1b	019c80f3-d111-7deb-a77a-d95cd7b0fad5	019b504c-ecdc-7447-a2e0-89f877b47d3b	t	\N	2026-02-21 17:06:28.369232+01	\N
019c80f3-d115-7818-97ad-dbcd9b34265a	019c80f3-d111-7deb-a77a-d95cd7b0fad5	019bb83f-9664-76e9-946d-d71c18f9ffab	t	\N	2026-02-21 17:06:28.369231+01	\N
019c80f3-d115-7cfe-b407-eaa288de3fcb	019c80f3-d111-7deb-a77a-d95cd7b0fad5	019c7091-37e2-780b-821f-33ba1efc0286	t	\N	2026-02-21 17:06:28.369232+01	\N
019c80f3-d115-7e96-9ce9-87cc54c8552b	019c80f3-d111-7deb-a77a-d95cd7b0fad5	019c8036-3652-7800-b41a-b748d77a590c	t	\N	2026-02-21 17:06:28.369232+01	\N
019c80fc-9d42-75e4-ab2c-d58b104044c3	019c80fc-9d42-7940-8279-58ec0d534495	019bb83c-738c-77f7-972c-122a8eb37195	t	\N	2026-02-21 17:16:04.930198+01	\N
019c80fc-9d42-77df-8582-1cfa325d75a9	019c80fc-9d42-7940-8279-58ec0d534495	019c8036-3652-7800-b41a-b748d77a590c	t	\N	2026-02-21 17:16:04.930197+01	\N
019c80fc-9d42-7b81-9460-46ea3a21a7a6	019c80fc-9d42-7940-8279-58ec0d534495	019b504c-ecdc-7447-a2e0-89f877b47d3b	t	\N	2026-02-21 17:16:04.930197+01	\N
019c80fc-9d42-7e7d-8ca5-d9183cdca670	019c80fc-9d42-7940-8279-58ec0d534495	019bb83f-9664-76e9-946d-d71c18f9ffab	t	\N	2026-02-21 17:16:04.930197+01	\N
019c80fc-9d42-7f16-90b6-acfa986dfb42	019c80fc-9d42-7940-8279-58ec0d534495	019bebd8-c71f-782b-8699-a2e71f54311a	t	\N	2026-02-21 17:16:04.930198+01	\N
019c80fe-2839-74dd-a28a-3dcaea6cae9d	019c80fe-2839-710e-9311-31676e2f6569	019bebd8-c71f-782b-8699-a2e71f54311a	t	\N	2026-02-21 17:17:46.041612+01	\N
019c80fe-2839-76e7-937e-2d3ab9cd8267	019c80fe-2839-710e-9311-31676e2f6569	019bb83c-738c-77f7-972c-122a8eb37195	t	\N	2026-02-21 17:17:46.041612+01	\N
019c80fe-2839-776f-b719-41f349d20703	019c80fe-2839-710e-9311-31676e2f6569	019b504c-ecdc-7447-a2e0-89f877b47d3b	t	\N	2026-02-21 17:17:46.041612+01	\N
019c80fe-2839-7996-b7d6-a74227e3e1ce	019c80fe-2839-710e-9311-31676e2f6569	019bb83f-9664-76e9-946d-d71c18f9ffab	t	\N	2026-02-21 17:17:46.041611+01	\N
019c80fe-2839-7e57-87da-5e5b2b77323f	019c80fe-2839-710e-9311-31676e2f6569	019c8036-3652-7800-b41a-b748d77a590c	t	\N	2026-02-21 17:17:46.041612+01	\N
\.


--
-- TOC entry 5273 (class 0 OID 26900)
-- Dependencies: 238
-- Data for Name: Offers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Offers" ("Id", "ItemId", "IsActive", "DateDeleted", "DateCreated", "DateEdited", "CreatedByUserId", "QuantityAvailable", "OfferAddressSnapshot_City", "OfferAddressSnapshot_Country", "OfferAddressSnapshot_FlatNumber", "OfferAddressSnapshot_HouseNumber", "OfferAddressSnapshot_PostalCity", "OfferAddressSnapshot_PostalCode", "OfferAddressSnapshot_Street", "Seller_Id", "Seller_Type", "Status") FROM stdin;
019c80f3-d111-7deb-a77a-d95cd7b0fad5	14f043bb-9264-4c97-97ba-23db8e5d5624	t	\N	2026-02-21 17:06:28.369124+01	2026-02-21 17:06:33.450408+01	90e966fd-ed39-4e59-be97-fdff12504616	15	Nowy Sacz	Poland	12	41	Nowy Sącz	33-300	Lwowska	effc9ac8-1c0b-4bd5-8c88-ff59eac28852	Company	Available
019c80f3-92c8-7022-93e7-7034044b80e6	14f043bb-9264-4c97-97ba-23db8e5d5624	t	\N	2026-02-21 17:06:12.42106+01	2026-02-21 17:06:36.136023+01	90e966fd-ed39-4e59-be97-fdff12504616	15	Nowy Sacz	Poland	12	41	Nowy Sącz	33-300	Lwowska	effc9ac8-1c0b-4bd5-8c88-ff59eac28852	Company	Available
019c80fc-9d42-7940-8279-58ec0d534495	1071ea1b-d757-44db-8122-8533e092fdad	t	\N	2026-02-21 17:16:04.930192+01	\N	280fa9bd-1d64-4f59-bb91-c6a539cdefba	50	Sloneczna	Poland	2	67	Kraków	33-666	Krakow	280fa9bd-1d64-4f59-bb91-c6a539cdefba	PrivatePerson	Available
019c80fe-2839-710e-9311-31676e2f6569	b1366a2e-1296-479a-8d06-bcd2b38a85d8	t	\N	2026-02-21 17:17:46.041608+01	\N	bd3395c3-83c0-4fd4-9d0a-42b4ab6320fc	10	Siedlce	Poland		89	Korzenna	33-322	Siedlce	bd3395c3-83c0-4fd4-9d0a-42b4ab6320fc	PrivatePerson	Available
\.


--
-- TOC entry 5274 (class 0 OID 26907)
-- Dependencies: 239
-- Data for Name: OrderDeliveries; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."OrderDeliveries" ("Id", "OrderId", "DeliveryName", "CarrierCode", "Channel", "Price_Amount", "Price_Currency", "Street", "HouseNumber", "FlatNumber", "City", "PostalCity", "PostalCode", "PickupPointId", "PickupPointName", "PickupStreet", "PickupCity", "PickupLocalNumber", "ParcelLockerId", "IsActive", "DateDeleted", "DateCreated", "DateEdited") FROM stdin;
\.


--
-- TOC entry 5275 (class 0 OID 26915)
-- Dependencies: 240
-- Data for Name: OrderLines; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."OrderLines" ("Id", "OrderId", "ItemName", "Thumbnail_ImagePath", "Thumbnail_AltText", "Quantity", "IsActive", "DateDeleted", "OrderLineType", "PricePerDay_Amount", "PricePerDay_Currency", "RentalDays", "PricePerItem_Amount", "PricePerItem_Currency", "DateCreated", "DateEdited", "OfferId") FROM stdin;
\.


--
-- TOC entry 5276 (class 0 OID 26920)
-- Dependencies: 241
-- Data for Name: Orders; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Orders" ("Id", "Total_Amount", "Total_Currency", "Status", "IsActive", "DateDeleted", "DateCreated", "DateEdited", "SellerSnapshot_Address_City", "SellerSnapshot_Address_Country", "SellerSnapshot_Address_FlatNumber", "SellerSnapshot_Address_HouseNumber", "SellerSnapshot_Address_PostalCode", "SellerSnapshot_Address_Street", "SellerSnapshot_Address_PostalCity", "BuyerId", "DateDelivered", "LinesTotal_Amount", "LinesTotal_Currency", "SellerSnapshot_DisplayName", "SellerSnapshot_SellerId", "SellerSnapshot_TIN", "SellerSnapshot_Type", "BuyerSnapshot_Address_City", "BuyerSnapshot_Address_Country", "BuyerSnapshot_Address_FlatNumber", "BuyerSnapshot_Address_HouseNumber", "BuyerSnapshot_Address_PostalCity", "BuyerSnapshot_Address_PostalCode", "BuyerSnapshot_Address_Street", "BuyerSnapshot_Email", "BuyerSnapshot_FullName", "BuyerSnapshot_PhoneNumber") FROM stdin;
\.


--
-- TOC entry 5289 (class 0 OID 27324)
-- Dependencies: 254
-- Data for Name: PaymentDetails; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."PaymentDetails" ("Id", "PaymentId", "Method", "PhoneNumber", "MaskedCardNumber", "CardHolderName", "DateCreated", "DateEdited", "DateDeleted", "IsActive") FROM stdin;
\.


--
-- TOC entry 5277 (class 0 OID 26939)
-- Dependencies: 242
-- Data for Name: PaymentOrders; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."PaymentOrders" ("Id", "PaymentId", "IsActive", "DateDeleted", "DateCreated", "DateEdited", "Amount_Amount", "Amount_Currency", "OrderId") FROM stdin;
\.


--
-- TOC entry 5278 (class 0 OID 26942)
-- Dependencies: 243
-- Data for Name: Payments; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Payments" ("Id", "Status", "Amount_Currency", "IsActive", "DateDeleted", "DateCreated", "DateEdited", "Amount_Amount", "ExternalTransactionId", "Method") FROM stdin;
\.


--
-- TOC entry 5279 (class 0 OID 26949)
-- Dependencies: 244
-- Data for Name: Permissions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Permissions" ("Id", "Name", "Description", "IsActive", "DateDeleted", "DateCreated", "DateEdited") FROM stdin;
cc8ce820-648a-4c1b-b948-6acd2d42edbf	company-info:create:one	Create company info	t	\N	2026-02-16 11:20:16.478133+01	\N
7f179e31-4197-4a21-bda8-f24a6e4bd40e	company-info:update:one	Update company info	t	\N	2026-02-16 11:20:16.478133+01	\N
176cd49a-cf34-460c-a133-1dbd49c1696a	company-categories:create:one	Create category	t	\N	2026-02-16 11:20:16.478133+01	\N
b4d92772-cff6-4172-9c57-d79b8e8d3f9d	company-categories:read:one	Read single category	t	\N	2026-02-16 11:20:16.478133+01	\N
c6298f91-4231-4bda-828f-d71ca896cd36	company-categories:update:one	Update category	t	\N	2026-02-16 11:20:16.478133+01	\N
b6a055b9-a2b2-466f-871a-e50ae7a217a0	company-categories:delete:one	Delete category	t	\N	2026-02-16 11:20:16.478133+01	\N
9fc0ef5f-bca6-4b24-a3ac-8f5dd7a54669	company-categories:read:many	List categories	t	\N	2026-02-16 11:20:16.478133+01	\N
19ee9066-c3fb-474e-abb1-38306f73b23a	company-conditions:create:one	Create condition	t	\N	2026-02-16 11:20:16.478133+01	\N
e456ad34-f11c-40a2-b398-5a031ff736c5	company-conditions:read:one	Read single condition	t	\N	2026-02-16 11:20:16.478133+01	\N
40237e18-dff7-48c3-a4da-f92624a3b335	company-conditions:update:one	Update condition	t	\N	2026-02-16 11:20:16.478133+01	\N
4e87f0e7-ee11-4df3-a667-f113585e5d8a	company-conditions:delete:one	Delete condition	t	\N	2026-02-16 11:20:16.478133+01	\N
980963f4-1b8b-45ff-b78d-3d1c027beab1	company-conditions:read:many	List conditions	t	\N	2026-02-16 11:20:16.478133+01	\N
f26b44f5-b188-49d7-9108-ab8d0f28ca88	company-countries:create:one	Create country	t	\N	2026-02-16 11:20:16.478133+01	\N
b4c1ee72-6daa-4e57-bf48-5e4eaffc636e	company-countries:read:one	Read single country	t	\N	2026-02-16 11:20:16.478133+01	\N
fccc4094-0cba-411d-83da-f283ee7b5a31	company-countries:update:one	Update country	t	\N	2026-02-16 11:20:16.478133+01	\N
9c9c5206-6551-4bfa-90ff-1713d9e1c5a0	company-countries:delete:one	Delete country	t	\N	2026-02-16 11:20:16.478133+01	\N
e8171dc1-2bbc-4249-b90c-e506f7acdea7	company-countries:read:many	List countries	t	\N	2026-02-16 11:20:16.478133+01	\N
32eccc2e-f468-4773-b464-33fac06229d1	company-deliveries:create:one	Create delivery	t	\N	2026-02-16 11:20:16.478133+01	\N
97f39eef-6efb-473c-9f00-56630772ba7c	company-deliveries:read:one	Read single delivery	t	\N	2026-02-16 11:20:16.478133+01	\N
4e2e674c-b5e2-4a0a-b087-cdb26a10917e	company-deliveries:update:one	Update delivery	t	\N	2026-02-16 11:20:16.478133+01	\N
254300e8-1473-421e-845c-0ebfa7931cfb	company-deliveries:delete:one	Delete delivery	t	\N	2026-02-16 11:20:16.478133+01	\N
a4a94ce9-a1c1-451a-b221-be6ae617ea62	company-deliveries:read:many	List deliveries	t	\N	2026-02-16 11:20:16.478133+01	\N
341bf0dd-e081-49af-80e6-f6baa5c14da1	company-deliveries:read:sizes	Get parcel locker sizes	t	\N	2026-02-16 11:20:16.478133+01	\N
c8c3ebe3-36d0-4b1d-a1d8-173d1fe0c08c	company-deliveries:read:channels	Get delivery channels	t	\N	2026-02-16 11:20:16.478133+01	\N
d75c269c-23f6-42ba-9bc5-d4a7bf8dc8b1	company-delivery-carriers:create:one	Create delivery carrier	t	\N	2026-02-16 11:20:16.478133+01	\N
06a2cb9c-bf0c-4161-88b4-7f3d272152dc	company-delivery-carriers:read:one	Read single delivery carrier	t	\N	2026-02-16 11:20:16.478133+01	\N
3700f441-531e-4e50-b54a-62565d74c52b	company-delivery-carriers:update:one	Update delivery carrier	t	\N	2026-02-16 11:20:16.478133+01	\N
09c97b8e-6747-41d6-a2cc-cd12eb610336	company-delivery-carriers:delete:one	Delete delivery carrier	t	\N	2026-02-16 11:20:16.478133+01	\N
9e4870fc-544e-4c48-8080-f0bd01fbf86c	company-delivery-carriers:read:many	List delivery carriers	t	\N	2026-02-16 11:20:16.478133+01	\N
5aedeb5c-5651-4b43-a364-6b16863e2b9b	company-delivery-carriers:read:options	Get delivery carriers options	t	\N	2026-02-16 11:20:16.478133+01	\N
541bbeba-78eb-482a-a16a-8c7e217aafb4	company-documents:read:order-details	Download order details PDF	t	\N	2026-02-16 11:20:16.478133+01	\N
5129bdb9-8ae7-4c92-9517-abb634b2d93b	company-employees:create:one	Create employee	t	\N	2026-02-16 11:20:16.478133+01	\N
2e8751d0-be49-4499-89b2-54f52eb6397c	company-employees:read:one	Read single employee	t	\N	2026-02-16 11:20:16.478133+01	\N
a9a0de3b-17a2-4bb4-8cd3-51670eef023b	company-employees:update:one	Update employee	t	\N	2026-02-16 11:20:16.478133+01	\N
a1d3e1cc-7819-4d30-bf9b-086d7f8e6669	company-employees:delete:one	Delete employee	t	\N	2026-02-16 11:20:16.478133+01	\N
06b72e90-34af-4f5c-9687-d3bf38361d80	company-employees:read:many	List employees	t	\N	2026-02-16 11:20:16.478133+01	\N
58357715-f090-420e-b647-da5b1b79ce49	company-employees:update:address	Update employee address	t	\N	2026-02-16 11:20:16.478133+01	\N
fed66b9f-eed6-412a-9a86-74dac8485a7f	company-items:create:one	Create item	t	\N	2026-02-16 11:20:16.478133+01	\N
a0a87fd7-c2a0-4dc4-829b-e6c9bd654f93	company-items:read:one	Read single item	t	\N	2026-02-16 11:20:16.478133+01	\N
13f7b8bd-e881-47c0-bf4a-fe21a472c1ae	company-items:update:one	Update item	t	\N	2026-02-16 11:20:16.478133+01	\N
90725bf8-58cb-48e5-ac88-18871f10a896	company-items:delete:one	Delete item	t	\N	2026-02-16 11:20:16.478133+01	\N
fa5e1d27-ee19-4551-847c-65dc3a560b0d	company-items:read:many	List items	t	\N	2026-02-16 11:20:16.478133+01	\N
6ca9c36e-3ea7-4275-8e46-6aeb87e096e4	company-orders:create:one	Create order	t	\N	2026-02-16 11:20:16.478133+01	\N
4ceee8dd-5e9d-4649-b589-4781d8f17160	company-orders:read:one	Read single order	t	\N	2026-02-16 11:20:16.478133+01	\N
dfe048c0-4cd8-4cda-8c1a-68ce32419ea8	company-orders:read:many	List orders	t	\N	2026-02-16 11:20:16.478133+01	\N
85b411b6-cf5b-4ebc-b69f-61a65e88bccd	company-orders:read:details	Read order details	t	\N	2026-02-16 11:20:16.478133+01	\N
dfb4541d-6d04-4976-a6ca-1163a79dab8d	company-orders:update:ship	Ship order	t	\N	2026-02-16 11:20:16.478133+01	\N
17387ae0-43ef-45c4-879f-99ea44f0a727	company-orders:update:deliver	Deliver order	t	\N	2026-02-16 11:20:16.478133+01	\N
2b434e2e-b17c-4293-8e13-6a227864653c	company-orders:read:dashboard	Read dashboard orders	t	\N	2026-02-16 11:20:16.478133+01	\N
7c17d8be-235a-4f5b-8406-4cd3101150f0	company-permissions:read:options	Get permissions options	t	\N	2026-02-16 11:20:16.478133+01	\N
0cb9af40-616c-4a7c-b323-3d4c5bfef50b	company-portalusers:create:one	Create portal user	t	\N	2026-02-16 11:20:16.478133+01	\N
3562efa0-d655-4ece-9692-130feaf98124	company-portalusers:read:one	Read single portal user	t	\N	2026-02-16 11:20:16.478133+01	\N
115e8d0f-3266-4c39-b5fb-1d88c5fa8af7	company-portalusers:update:one	Update portal user	t	\N	2026-02-16 11:20:16.478133+01	\N
df8eb3c8-92d4-47ed-b1ef-d0220f04f272	company-portalusers:delete:one	Delete portal user	t	\N	2026-02-16 11:20:16.478133+01	\N
b931c4e0-be92-467c-b3cb-e52204e2fa39	company-portalusers:read:many	List portal users	t	\N	2026-02-16 11:20:16.478133+01	\N
8a6fd0d6-1baf-497d-9f47-99970aa0ef5a	company-rent-offers:create:one	Create rent offer	t	\N	2026-02-16 11:20:16.478133+01	\N
daef4083-2b1b-42c1-8050-a19cb2febfc6	company-rent-offers:read:one	Read single rent offer	t	\N	2026-02-16 11:20:16.478133+01	\N
5b8aebac-e7a6-4911-938f-f19f1d450927	company-rent-offers:update:one	Update rent offer	t	\N	2026-02-16 11:20:16.478133+01	\N
0c2b88ff-cd57-4504-8124-4c76cfe73b1e	company-rent-offers:delete:one	Delete rent offer	t	\N	2026-02-16 11:20:16.478133+01	\N
5f22fc2b-e663-45f1-bc15-cfb5c136f280	company-rent-offers:read:many	List rent offers	t	\N	2026-02-16 11:20:16.478133+01	\N
bf815458-7960-4dce-912a-67df8d350f1d	company-rentals:read:many	List company rentals	t	\N	2026-02-16 11:20:16.478133+01	\N
917bb52b-e16e-4e09-aafb-65dc7571d543	company-rentals:read:one	Read single rental	t	\N	2026-02-16 11:20:16.478133+01	\N
8e8b5b0f-7d95-455c-8be0-bf9cf73f4561	company-roles:create:one	Create role	t	\N	2026-02-16 11:20:16.478133+01	\N
675ae22a-3a1e-42d7-b731-1ed0c90afe53	company-roles:read:one	Read single role	t	\N	2026-02-16 11:20:16.478133+01	\N
9d53ed40-2147-48a0-aada-d06c9aa542dd	company-roles:update:one	Update role	t	\N	2026-02-16 11:20:16.478133+01	\N
eedf6023-1dda-430a-aa63-84b239217c92	company-roles:delete:one	Delete role	t	\N	2026-02-16 11:20:16.478133+01	\N
46294206-dcfc-45be-ad5a-35794fcf216b	company-roles:read:options	Get roles options	t	\N	2026-02-16 11:20:16.478133+01	\N
ae45f4f8-15c4-431d-86de-33c41991fe9f	company-roles:read:many	List roles	t	\N	2026-02-16 11:20:16.478133+01	\N
e0420f2b-8677-468d-abff-c65fa4224cf8	company-sale-offers:create:one	Create sale offer	t	\N	2026-02-16 11:20:16.478133+01	\N
a7b5b2c7-77b6-4d0d-a177-ee640edf5c61	company-sale-offers:read:one	Read single sale offer	t	\N	2026-02-16 11:20:16.478133+01	\N
5554b3c8-e88e-4a88-b4b8-14a601f0d3e5	company-orders:update:one	Update order	f	\N	2026-02-16 11:20:16.478133+01	\N
1b7a8176-c19b-42e1-841a-6a635059393e	company-orders:delete:one	Delete order	f	\N	2026-02-16 11:20:16.478133+01	\N
927398e9-d97e-4491-9834-1c7dad4fc6e7	company-sale-offers:update:one	Update sale offer	t	\N	2026-02-16 11:20:16.478133+01	\N
1bffb46e-64ff-40f9-8f22-e65dd9d607e7	company-sale-offers:delete:one	Delete sale offer	t	\N	2026-02-16 11:20:16.478133+01	\N
4d7c6048-482f-45eb-b163-1b515178a016	company-sale-offers:read:many	List sale offers	t	\N	2026-02-16 11:20:16.478133+01	\N
d2d71325-d06a-4af1-827b-258a88fed1f2	company-statistics:read:kpi	Read KPI statistics	t	\N	2026-02-16 11:20:16.478133+01	\N
590ed689-3fed-41fd-b913-a92cae299391	company-statistics:read:gmv-seller-type	Read GMV by seller type	t	\N	2026-02-16 11:20:16.478133+01	\N
318324ef-63b3-4461-8f05-a9d9646d58cf	company-statistics:read:gmv-months	Read GMV by months	t	\N	2026-02-16 11:20:16.478133+01	\N
4b8cd345-a457-4e6a-9f4d-86a3e7e25144	company-user-home-addresses:update:one	Update user home address	t	\N	2026-02-16 11:20:16.478133+01	\N
9a359d02-70f4-4fd2-994f-e75802b757aa	user:read:one	Read user profile	t	\N	2026-02-16 11:20:16.478133+01	\N
57e53af8-de3b-486b-8560-3ad6bc1f1786	user:update:one	Update user profile	t	\N	2026-02-16 11:20:16.478133+01	\N
52ce1fcc-707b-4b32-afb4-aa67994f2cad	user-carts:delete:one	Remove item from cart	t	\N	2026-02-16 11:20:16.478133+01	\N
53df4388-3d1f-4cfe-ab06-71f0cd1e7be2	user-carts:create:rent	Add rent offer to cart	t	\N	2026-02-16 11:20:16.478133+01	\N
4c58d31a-0258-4fc7-abbe-4177b9f3cdbd	user-carts:create:sale	Add sale offer to cart	t	\N	2026-02-16 11:20:16.478133+01	\N
546e6834-9c20-4b5f-9766-2190d9932e52	user-carts:update:sale	Update sale offer in cart	t	\N	2026-02-16 11:20:16.478133+01	\N
14fdc5d2-6e87-4b38-873b-650c9bfb7707	user-carts:update:rent	Update rent offer in cart	t	\N	2026-02-16 11:20:16.478133+01	\N
0cffd856-f777-45e6-8855-9895631ff787	user-carts:read:one	Read cart	t	\N	2026-02-16 11:20:16.478133+01	\N
641a636b-5f28-42d1-8acb-56e5498beef2	user-carts:delete:all	Clear cart	t	\N	2026-02-16 11:20:16.478133+01	\N
8c63d7bf-fb94-4678-bab6-53a08d43ca2d	user-checkout:read:one	Read checkout	t	\N	2026-02-16 11:20:16.478133+01	\N
2f42a57a-2589-4035-ae22-570e6099eeef	user-employee:read:one	Read employee profile	t	\N	2026-02-16 11:20:16.478133+01	\N
45d6a786-a896-4e13-a6b2-a7d37fd3027f	user-home-address:read:one	Read home address	t	\N	2026-02-16 11:20:16.478133+01	\N
df1afe1a-8b49-4ac3-bcf3-71337b86b805	user-home-address:update:one	Update home address	t	\N	2026-02-16 11:20:16.478133+01	\N
19357300-48b8-4d64-a835-d695f31291e1	user-offers:read:many	List user offers	t	\N	2026-02-16 11:20:16.478133+01	\N
fa9694aa-e757-423e-8c17-707f14403298	user-orders:create:one	Create order	t	\N	2026-02-16 11:20:16.478133+01	\N
a637dbdb-8be2-4446-beaa-69c3c05aa3bf	user-orders:read:many	List user orders	t	\N	2026-02-16 11:20:16.478133+01	\N
1a45d107-e4b8-4ef5-a8ed-e5694bcf8da5	user-orders:read:seller	List seller orders	t	\N	2026-02-16 11:20:16.478133+01	\N
b9b9e576-9119-443f-9f0c-6d96a8240199	user-orders:read:details	Read order details	t	\N	2026-02-16 11:20:16.478133+01	\N
e94b77d5-d98f-4574-9792-47ed3a3fe2e8	user-orders:update:cancel	Cancel order	t	\N	2026-02-16 11:20:16.478133+01	\N
53f849a7-3365-4397-9eed-45a17b400335	user-orders:update:return	Return order	t	\N	2026-02-16 11:20:16.478133+01	\N
b240b3bf-ab1f-42e3-89f7-25c23f980c32	user-orders:update:ship	Ship order	t	\N	2026-02-16 11:20:16.478133+01	\N
5d821c3d-e07e-4dae-8759-30ac2976bf57	user-orders:update:deliver	Deliver order	t	\N	2026-02-16 11:20:16.478133+01	\N
062fe4b1-4c4d-4d43-b5ec-991a61093992	user-payments:read:one	Read payment	t	\N	2026-02-16 11:20:16.478133+01	\N
1f196dd3-c87a-48bf-90d8-432e9f8857a5	user-payments:update:blik	Pay via BLIK	t	\N	2026-02-16 11:20:16.478133+01	\N
c6a616de-c18c-4b7d-a450-7b729b58ed14	user-payments:update:card	Pay via card	t	\N	2026-02-16 11:20:16.478133+01	\N
0313ff3d-90c5-466c-bba1-ca94335dec65	users:update:password	Change password	t	\N	2026-02-16 11:20:16.478133+01	\N
21e734c8-26c0-4c02-9c76-a698d4e8aab3	user-rentals:read:borrower	List borrower rentals	t	\N	2026-02-16 11:20:16.478133+01	\N
50c20831-8117-42eb-a11b-75fa1e5a1607	user-rentals:read:lender	List lender rentals	t	\N	2026-02-16 11:20:16.478133+01	\N
03318db7-e246-4fce-a908-b9a43a95e930	user-rentals:update:return	Return rented item	t	\N	2026-02-16 11:20:16.478133+01	\N
2d3979c7-2aba-405a-8df4-5d1b91da9084	user-rent-offers:create:one	Create rent offer	t	\N	2026-02-16 11:20:16.478133+01	\N
40e89974-8eba-454e-b08d-1f7cdd8433b2	user-rent-offers:read:one	Read rent offer	t	\N	2026-02-16 11:20:16.478133+01	\N
b94e8e9e-4cf9-4fac-a581-3a232549b863	user-rent-offers:update:one	Update rent offer	t	\N	2026-02-16 11:20:16.478133+01	\N
544bf5b0-0d6a-424b-9f4d-c318f725ed22	user-rent-offers:delete:one	Delete rent offer	t	\N	2026-02-16 11:20:16.478133+01	\N
8c0dac82-7234-4cfa-bc60-53e384fdee1a	user-sale-offers:create:one	Create sale offer	t	\N	2026-02-16 11:20:16.478133+01	\N
2132a9df-1ab7-4b61-a96a-e4c3184c8f17	user-sale-offers:read:one	Read sale offer	t	\N	2026-02-16 11:20:16.478133+01	\N
291b4c66-3cb9-4a40-b21f-e7c645dc7af7	user-sale-offers:update:one	Update sale offer	t	\N	2026-02-16 11:20:16.478133+01	\N
c46f27d6-8385-4185-9255-408e41e7e6f9	user-sale-offers:delete:one	Delete sale offer	t	\N	2026-02-16 11:20:16.478133+01	\N
758c857b-bb75-4b9a-a8d3-02936ebde21c	user-shipping-addresses:create:one	Create shipping address	t	\N	2026-02-16 11:20:16.478133+01	\N
f98bc1b0-6bf1-4e89-95b9-439cd75fdb63	user-shipping-addresses:read:one	Read shipping address	t	\N	2026-02-16 11:20:16.478133+01	\N
84d6ca18-3f5c-4b73-88b0-9ab8c5766ca4	user-shipping-addresses:update:one	Update shipping address	t	\N	2026-02-16 11:20:16.478133+01	\N
60f2e0af-4b40-4d63-a611-cf5963f16a69	user-shipping-addresses:delete:one	Delete shipping address	t	\N	2026-02-16 11:20:16.478133+01	\N
7653f370-529e-4fd0-9790-07cacb317ebf	user-shipping-addresses:read:many	List shipping addresses	t	\N	2026-02-16 11:20:16.478133+01	\N
091283c5-7336-414b-b05f-e7d13d7657a3	user-shipping-addresses:read:checkout	Read checkout address	t	\N	2026-02-16 11:20:16.478133+01	\N
\.


--
-- TOC entry 5280 (class 0 OID 26954)
-- Dependencies: 245
-- Data for Name: RentCartOffers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."RentCartOffers" ("Id", "RentalDays") FROM stdin;
\.


--
-- TOC entry 5281 (class 0 OID 26958)
-- Dependencies: 246
-- Data for Name: RentOffers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."RentOffers" ("Id", "PricePerDay_Amount", "PricePerDay_Currency", "MaxRentalDays") FROM stdin;
019c80f3-92c8-7022-93e7-7034044b80e6	10.000	PLN	50
019c80fe-2839-710e-9311-31676e2f6569	2.000	PLN	20
\.


--
-- TOC entry 5282 (class 0 OID 26961)
-- Dependencies: 247
-- Data for Name: Rentals; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Rentals" ("Id", "RentOrderLineId", "RentalStartDate", "RentalEndDate", "ReturnedDate", "IsActive", "DateDeleted", "DateCreated", "DateEdited", "BorrowerId", "Lender_SellerId", "Lender_Type", "PricePerDay_Amount", "PricePerDay_Currency", "RentalDays", "Status", "DeliveryDate", "ItemName", "Lender_Address_City", "Lender_Address_Country", "Lender_Address_FlatNumber", "Lender_Address_HouseNumber", "Lender_Address_PostalCity", "Lender_Address_PostalCode", "Lender_Address_Street", "Lender_DisplayName", "Lender_TIN", "Quantity", "Thumbnail_AltText", "Thumbnail_ImagePath") FROM stdin;
\.


--
-- TOC entry 5283 (class 0 OID 26971)
-- Dependencies: 248
-- Data for Name: RolePermissions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."RolePermissions" ("Id", "RoleId", "PermissionId", "IsActive", "DateDeleted", "DateCreated", "DateEdited") FROM stdin;
019c660b-eb04-7b56-917d-35921fb63e6a	019c49b1-4170-779e-85d2-6a2e26582981	3700f441-531e-4e50-b54a-62565d74c52b	t	\N	2026-02-16 11:43:03.000689+01	\N
019c660b-eaf8-7d33-908b-9478809c15b3	019c49b1-4170-779e-85d2-6a2e26582981	cc8ce820-648a-4c1b-b948-6acd2d42edbf	t	\N	2026-02-16 11:43:03.000558+01	\N
019c660b-eb04-7090-a4ec-e98a112113cb	019c49b1-4170-779e-85d2-6a2e26582981	e8171dc1-2bbc-4249-b90c-e506f7acdea7	t	\N	2026-02-16 11:43:03.000686+01	\N
019c660b-eb04-7098-aa3e-813a31815d6f	019c49b1-4170-779e-85d2-6a2e26582981	06b72e90-34af-4f5c-9687-d3bf38361d80	t	\N	2026-02-16 11:43:03.000693+01	\N
019c660b-eb04-7135-9524-7d6802d64e37	019c49b1-4170-779e-85d2-6a2e26582981	32eccc2e-f468-4773-b464-33fac06229d1	t	\N	2026-02-16 11:43:03.000687+01	\N
019c660b-eb04-71ca-9cb0-ddac6a3cc61d	019c49b1-4170-779e-85d2-6a2e26582981	980963f4-1b8b-45ff-b78d-3d1c027beab1	t	\N	2026-02-16 11:43:03.000685+01	\N
019c660b-eb04-726c-b6dd-9215c1f3e1a8	019c49b1-4170-779e-85d2-6a2e26582981	c6298f91-4231-4bda-828f-d71ca896cd36	t	\N	2026-02-16 11:43:03.000684+01	\N
019c660b-eb04-72c8-a823-ca8d1ad56dc6	019c49b1-4170-779e-85d2-6a2e26582981	341bf0dd-e081-49af-80e6-f6baa5c14da1	t	\N	2026-02-16 11:43:03.000688+01	\N
019c660b-eb04-734c-b267-09c68af48e76	019c49b1-4170-779e-85d2-6a2e26582981	f26b44f5-b188-49d7-9108-ab8d0f28ca88	t	\N	2026-02-16 11:43:03.000685+01	\N
019c660b-eb04-737b-886c-c6385ca4e855	019c49b1-4170-779e-85d2-6a2e26582981	e456ad34-f11c-40a2-b398-5a031ff736c5	t	\N	2026-02-16 11:43:03.000685+01	\N
019c660b-eb04-7396-948b-72058a7d2a01	019c49b1-4170-779e-85d2-6a2e26582981	254300e8-1473-421e-845c-0ebfa7931cfb	t	\N	2026-02-16 11:43:03.000687+01	\N
019c660b-eb04-73c2-b367-f36ab630b754	019c49b1-4170-779e-85d2-6a2e26582981	a0a87fd7-c2a0-4dc4-829b-e6c9bd654f93	t	\N	2026-02-16 11:43:03.000694+01	\N
019c660b-eb04-73e4-a9c0-7aa0fe06c1cd	019c49b1-4170-779e-85d2-6a2e26582981	1b7a8176-c19b-42e1-841a-6a635059393e	t	\N	2026-02-16 11:43:03.000697+01	\N
019c660b-eb04-73f4-ab68-bf8cd289bd6a	019c49b1-4170-779e-85d2-6a2e26582981	13f7b8bd-e881-47c0-bf4a-fe21a472c1ae	t	\N	2026-02-16 11:43:03.000694+01	\N
019c660b-eb04-74f4-8fa4-0140f904c03a	019c49b1-4170-779e-85d2-6a2e26582981	40237e18-dff7-48c3-a4da-f92624a3b335	t	\N	2026-02-16 11:43:03.000685+01	\N
019c660b-eb04-74fe-9283-cf492603c2ee	019c49b1-4170-779e-85d2-6a2e26582981	17387ae0-43ef-45c4-879f-99ea44f0a727	t	\N	2026-02-16 11:43:03.000703+01	\N
019c660b-eb04-7696-8499-11f2b958c359	019c49b1-4170-779e-85d2-6a2e26582981	a9a0de3b-17a2-4bb4-8cd3-51670eef023b	t	\N	2026-02-16 11:43:03.000691+01	\N
019c660b-eb04-76d1-aab3-852ec4cf28f1	019c49b1-4170-779e-85d2-6a2e26582981	b4d92772-cff6-4172-9c57-d79b8e8d3f9d	t	\N	2026-02-16 11:43:03.000683+01	\N
019c660b-eb04-7714-b910-abed839c22fd	019c49b1-4170-779e-85d2-6a2e26582981	06a2cb9c-bf0c-4161-88b4-7f3d272152dc	t	\N	2026-02-16 11:43:03.000689+01	\N
019c660b-eb04-7715-a583-c4c85ee2dbda	019c49b1-4170-779e-85d2-6a2e26582981	19ee9066-c3fb-474e-abb1-38306f73b23a	t	\N	2026-02-16 11:43:03.000684+01	\N
019c660b-eb04-77ee-a559-b201b7598cb1	019c49b1-4170-779e-85d2-6a2e26582981	7f179e31-4197-4a21-bda8-f24a6e4bd40e	t	\N	2026-02-16 11:43:03.000682+01	\N
019c660b-eb04-77f5-917c-19bd24b004d6	019c49b1-4170-779e-85d2-6a2e26582981	fccc4094-0cba-411d-83da-f283ee7b5a31	t	\N	2026-02-16 11:43:03.000686+01	\N
019c660b-eb04-7819-9fcd-c615aa64b379	019c49b1-4170-779e-85d2-6a2e26582981	b6a055b9-a2b2-466f-871a-e50ae7a217a0	t	\N	2026-02-16 11:43:03.000684+01	\N
019c660b-eb04-7911-b9ef-47d0587541d1	019c49b1-4170-779e-85d2-6a2e26582981	6ca9c36e-3ea7-4275-8e46-6aeb87e096e4	t	\N	2026-02-16 11:43:03.000696+01	\N
019c660b-eb04-797b-9aeb-6412f70e4d1c	019c49b1-4170-779e-85d2-6a2e26582981	5129bdb9-8ae7-4c92-9517-abb634b2d93b	t	\N	2026-02-16 11:43:03.000691+01	\N
019c660b-eb04-7982-b175-ce201c1d40da	019c49b1-4170-779e-85d2-6a2e26582981	dfb4541d-6d04-4976-a6ca-1163a79dab8d	t	\N	2026-02-16 11:43:03.000703+01	\N
019c660b-eb04-79ac-857a-d89ab7eed2e0	019c49b1-4170-779e-85d2-6a2e26582981	541bbeba-78eb-482a-a16a-8c7e217aafb4	t	\N	2026-02-16 11:43:03.00069+01	\N
019c660b-eb04-79b8-af53-a7cd2c367a42	019c49b1-4170-779e-85d2-6a2e26582981	5554b3c8-e88e-4a88-b4b8-14a601f0d3e5	t	\N	2026-02-16 11:43:03.000697+01	\N
019c660b-eb04-79c9-8752-f72f1fdd7ba5	019c49b1-4170-779e-85d2-6a2e26582981	9fc0ef5f-bca6-4b24-a3ac-8f5dd7a54669	t	\N	2026-02-16 11:43:03.000684+01	\N
019c660b-eb04-7a44-8e8d-7175b7a06377	019c49b1-4170-779e-85d2-6a2e26582981	fa5e1d27-ee19-4551-847c-65dc3a560b0d	t	\N	2026-02-16 11:43:03.000695+01	\N
019c660b-eb04-7a47-a4a2-94880ab117b7	019c49b1-4170-779e-85d2-6a2e26582981	5aedeb5c-5651-4b43-a364-6b16863e2b9b	t	\N	2026-02-16 11:43:03.00069+01	\N
019c660b-eb04-7a56-92e9-1ad2650817db	019c49b1-4170-779e-85d2-6a2e26582981	176cd49a-cf34-460c-a133-1dbd49c1696a	t	\N	2026-02-16 11:43:03.000682+01	\N
019c660b-eb04-7ab2-9a35-7371d777f686	019c49b1-4170-779e-85d2-6a2e26582981	97f39eef-6efb-473c-9f00-56630772ba7c	t	\N	2026-02-16 11:43:03.000687+01	\N
019c660b-eb04-7acf-b538-b5e8bd084d25	019c49b1-4170-779e-85d2-6a2e26582981	90725bf8-58cb-48e5-ac88-18871f10a896	t	\N	2026-02-16 11:43:03.000695+01	\N
019c660b-eb04-7aea-93f7-932c1f5f1c58	019c49b1-4170-779e-85d2-6a2e26582981	d75c269c-23f6-42ba-9bc5-d4a7bf8dc8b1	t	\N	2026-02-16 11:43:03.000688+01	\N
019c660b-eb04-7c21-b152-19ba6244c280	019c49b1-4170-779e-85d2-6a2e26582981	85b411b6-cf5b-4ebc-b69f-61a65e88bccd	t	\N	2026-02-16 11:43:03.000698+01	\N
019c660b-eb04-7c42-bfe5-89b854403b4d	019c49b1-4170-779e-85d2-6a2e26582981	9e4870fc-544e-4c48-8080-f0bd01fbf86c	t	\N	2026-02-16 11:43:03.00069+01	\N
019c660b-eb04-7c88-b1fe-c9dea814f4ae	019c49b1-4170-779e-85d2-6a2e26582981	fed66b9f-eed6-412a-9a86-74dac8485a7f	t	\N	2026-02-16 11:43:03.000694+01	\N
019c660b-eb04-7d20-b6f3-f1ad969439d4	019c49b1-4170-779e-85d2-6a2e26582981	c8c3ebe3-36d0-4b1d-a1d8-173d1fe0c08c	t	\N	2026-02-16 11:43:03.000688+01	\N
019c660b-eb04-7d22-b4a4-004c11be057c	019c49b1-4170-779e-85d2-6a2e26582981	58357715-f090-420e-b647-da5b1b79ce49	t	\N	2026-02-16 11:43:03.000693+01	\N
019c660b-eb04-7d30-bd0b-3d0f926e4be8	019c49b1-4170-779e-85d2-6a2e26582981	a4a94ce9-a1c1-451a-b221-be6ae617ea62	t	\N	2026-02-16 11:43:03.000688+01	\N
019c660b-eb04-7d74-80a0-4c358f00c22d	019c49b1-4170-779e-85d2-6a2e26582981	4e2e674c-b5e2-4a0a-b087-cdb26a10917e	t	\N	2026-02-16 11:43:03.000687+01	\N
019c660b-eb04-7dcf-b6e8-526dcf7911bb	019c49b1-4170-779e-85d2-6a2e26582981	09c97b8e-6747-41d6-a2cc-cd12eb610336	t	\N	2026-02-16 11:43:03.000689+01	\N
019c660b-eb04-7e72-9a89-a602cc5ca8ac	019c49b1-4170-779e-85d2-6a2e26582981	a1d3e1cc-7819-4d30-bf9b-086d7f8e6669	t	\N	2026-02-16 11:43:03.000692+01	\N
019c660b-eb04-7eba-83fc-74a319c3dbe4	019c49b1-4170-779e-85d2-6a2e26582981	2e8751d0-be49-4499-89b2-54f52eb6397c	t	\N	2026-02-16 11:43:03.000691+01	\N
019c660b-eb04-7ecb-918a-0e53a3fffdf6	019c49b1-4170-779e-85d2-6a2e26582981	4e87f0e7-ee11-4df3-a667-f113585e5d8a	t	\N	2026-02-16 11:43:03.000685+01	\N
019c660b-eb04-7f8f-8fa0-4151b2571048	019c49b1-4170-779e-85d2-6a2e26582981	9c9c5206-6551-4bfa-90ff-1713d9e1c5a0	t	\N	2026-02-16 11:43:03.000686+01	\N
019c660b-eb04-7fd0-a49a-75a279486ce9	019c49b1-4170-779e-85d2-6a2e26582981	dfe048c0-4cd8-4cda-8c1a-68ce32419ea8	t	\N	2026-02-16 11:43:03.000698+01	\N
019c660b-eb04-7fdf-82d8-97f8d4a7186b	019c49b1-4170-779e-85d2-6a2e26582981	4ceee8dd-5e9d-4649-b589-4781d8f17160	t	\N	2026-02-16 11:43:03.000696+01	\N
019c660b-eb04-7ffb-bf9c-b53af3109f0d	019c49b1-4170-779e-85d2-6a2e26582981	b4c1ee72-6daa-4e57-bf48-5e4eaffc636e	t	\N	2026-02-16 11:43:03.000686+01	\N
019c660b-eb05-71cd-8a14-67da56bbf97f	019c49b1-4170-779e-85d2-6a2e26582981	b931c4e0-be92-467c-b3cb-e52204e2fa39	t	\N	2026-02-16 11:43:03.000707+01	\N
019c660b-eb05-71e6-be08-d68e2fc9d73e	019c49b1-4170-779e-85d2-6a2e26582981	5f22fc2b-e663-45f1-bc15-cfb5c136f280	t	\N	2026-02-16 11:43:03.00071+01	\N
019c660b-eb05-721c-90ce-7dc557cf3f4f	019c49b1-4170-779e-85d2-6a2e26582981	daef4083-2b1b-42c1-8050-a19cb2febfc6	t	\N	2026-02-16 11:43:03.000708+01	\N
019c660b-eb05-7230-baa7-8aff74dedad9	019c49b1-4170-779e-85d2-6a2e26582981	2b434e2e-b17c-4293-8e13-6a227864653c	t	\N	2026-02-16 11:43:03.000704+01	\N
019c660b-eb05-72a7-951d-97d239b12ace	019c49b1-4170-779e-85d2-6a2e26582981	8a6fd0d6-1baf-497d-9f47-99970aa0ef5a	t	\N	2026-02-16 11:43:03.000708+01	\N
019c660b-eb05-72ef-91b2-2bc75511a9d3	019c49b1-4170-779e-85d2-6a2e26582981	df8eb3c8-92d4-47ed-b1ef-d0220f04f272	t	\N	2026-02-16 11:43:03.000707+01	\N
019c660b-eb05-731a-9d73-0df443541587	019c49b1-4170-779e-85d2-6a2e26582981	115e8d0f-3266-4c39-b5fb-1d88c5fa8af7	t	\N	2026-02-16 11:43:03.000706+01	\N
019c660b-eb05-736f-8e22-77d0bca9d8c4	019c49b1-4170-779e-85d2-6a2e26582981	3562efa0-d655-4ece-9692-130feaf98124	t	\N	2026-02-16 11:43:03.000705+01	\N
019c660b-eb05-73a0-a99d-2d4c684a0322	019c49b1-4170-779e-85d2-6a2e26582981	675ae22a-3a1e-42d7-b731-1ed0c90afe53	t	\N	2026-02-16 11:43:03.000712+01	\N
019c660b-eb05-73b5-b048-636703be786a	019c49b1-4170-779e-85d2-6a2e26582981	bf815458-7960-4dce-912a-67df8d350f1d	t	\N	2026-02-16 11:43:03.000711+01	\N
019c660b-eb05-7668-88bc-533f62a902bc	019c49b1-4170-779e-85d2-6a2e26582981	8e8b5b0f-7d95-455c-8be0-bf9cf73f4561	t	\N	2026-02-16 11:43:03.000712+01	\N
019c660b-eb05-7692-a65e-1f4c10279de3	019c49b1-4170-779e-85d2-6a2e26582981	9d53ed40-2147-48a0-aada-d06c9aa542dd	t	\N	2026-02-16 11:43:03.000714+01	\N
019c660b-eb05-785f-bcd2-14510252ec56	019c49b1-4170-779e-85d2-6a2e26582981	5b8aebac-e7a6-4911-938f-f19f1d450927	t	\N	2026-02-16 11:43:03.000709+01	\N
019c660b-eb05-79d2-a719-b513d1d0ed8f	019c49b1-4170-779e-85d2-6a2e26582981	0c2b88ff-cd57-4504-8124-4c76cfe73b1e	t	\N	2026-02-16 11:43:03.000709+01	\N
019c660b-eb05-7e29-8abc-ed6c5b163ac0	019c49b1-4170-779e-85d2-6a2e26582981	917bb52b-e16e-4e09-aafb-65dc7571d543	t	\N	2026-02-16 11:43:03.000711+01	\N
019c660b-eb06-7102-9035-348fc237299f	019c49b1-4170-779e-85d2-6a2e26582981	ae45f4f8-15c4-431d-86de-33c41991fe9f	t	\N	2026-02-16 11:43:03.000716+01	\N
019c660b-eb06-71dc-9293-3fbf4fe47e0e	019c49b1-4170-779e-85d2-6a2e26582981	e0420f2b-8677-468d-abff-c65fa4224cf8	t	\N	2026-02-16 11:43:03.000716+01	\N
019c660b-eb06-7275-84a1-95c04cbae352	019c49b1-4170-779e-85d2-6a2e26582981	a7b5b2c7-77b6-4d0d-a177-ee640edf5c61	t	\N	2026-02-16 11:43:03.000717+01	\N
019c660b-eb06-72fd-87b1-f3207334011a	019c49b1-4170-779e-85d2-6a2e26582981	1bffb46e-64ff-40f9-8f22-e65dd9d607e7	t	\N	2026-02-16 11:43:03.000718+01	\N
019c660b-eb06-767c-b547-fb6602ee8f82	019c49b1-4170-779e-85d2-6a2e26582981	927398e9-d97e-4491-9834-1c7dad4fc6e7	t	\N	2026-02-16 11:43:03.000718+01	\N
019c660b-eb06-7c6b-8385-223e6aa48866	019c49b1-4170-779e-85d2-6a2e26582981	46294206-dcfc-45be-ad5a-35794fcf216b	t	\N	2026-02-16 11:43:03.000715+01	\N
019c660b-eb06-7d3e-ae84-5d4e4a577243	019c49b1-4170-779e-85d2-6a2e26582981	eedf6023-1dda-430a-aa63-84b239217c92	t	\N	2026-02-16 11:43:03.000714+01	\N
019c660b-eb07-7073-b389-4c1b569c3a88	019c49b1-4170-779e-85d2-6a2e26582981	590ed689-3fed-41fd-b913-a92cae299391	t	\N	2026-02-16 11:43:03.00072+01	\N
019c660b-eb07-75c4-93a3-83195b36dd6c	019c49b1-4170-779e-85d2-6a2e26582981	318324ef-63b3-4461-8f05-a9d9646d58cf	t	\N	2026-02-16 11:43:03.000721+01	\N
019c660b-eb07-7e69-bfe3-3d5d9657a631	019c49b1-4170-779e-85d2-6a2e26582981	d2d71325-d06a-4af1-827b-258a88fed1f2	t	\N	2026-02-16 11:43:03.00072+01	\N
019c660b-eb07-7efb-842b-428bd69402a9	019c49b1-4170-779e-85d2-6a2e26582981	4d7c6048-482f-45eb-b163-1b515178a016	t	\N	2026-02-16 11:43:03.000719+01	\N
019c660c-b51f-703f-989f-d736b74b9f23	019a6de8-1bbf-7715-9eb1-0f800e19395a	c46f27d6-8385-4185-9255-408e41e7e6f9	t	\N	2026-02-16 11:43:54.782023+01	\N
019c660c-b51f-704e-9e58-34d3fd92ae89	019a6de8-1bbf-7715-9eb1-0f800e19395a	40e89974-8eba-454e-b08d-1f7cdd8433b2	t	\N	2026-02-16 11:43:54.78202+01	\N
019c660c-b51f-704f-bf27-13a7f088e631	019a6de8-1bbf-7715-9eb1-0f800e19395a	53f849a7-3365-4397-9eed-45a17b400335	t	\N	2026-02-16 11:43:54.782015+01	\N
019c660c-b51f-7052-8788-8696d1356141	019a6de8-1bbf-7715-9eb1-0f800e19395a	9a359d02-70f4-4fd2-994f-e75802b757aa	t	\N	2026-02-16 11:43:54.782009+01	\N
019c660c-b51f-7077-b805-7fcd510eaf49	019a6de8-1bbf-7715-9eb1-0f800e19395a	21e734c8-26c0-4c02-9c76-a698d4e8aab3	t	\N	2026-02-16 11:43:54.782017+01	\N
019c660c-b51f-7137-b3d4-81a9a6d7eca3	019a6de8-1bbf-7715-9eb1-0f800e19395a	53df4388-3d1f-4cfe-ab06-71f0cd1e7be2	t	\N	2026-02-16 11:43:54.78201+01	\N
019c660c-b51f-7280-b43b-a7c64f698c7b	019a6de8-1bbf-7715-9eb1-0f800e19395a	1f196dd3-c87a-48bf-90d8-432e9f8857a5	t	\N	2026-02-16 11:43:54.782016+01	\N
019c660c-b51f-72bf-87bb-cbfc5238a5b6	019a6de8-1bbf-7715-9eb1-0f800e19395a	52ce1fcc-707b-4b32-afb4-aa67994f2cad	t	\N	2026-02-16 11:43:54.78201+01	\N
019c660c-b51f-72c2-b812-7a9734d9825f	019a6de8-1bbf-7715-9eb1-0f800e19395a	b94e8e9e-4cf9-4fac-a581-3a232549b863	t	\N	2026-02-16 11:43:54.782021+01	\N
019c660c-b51f-72f6-bbde-d12e91e172a4	019a6de8-1bbf-7715-9eb1-0f800e19395a	e94b77d5-d98f-4574-9792-47ed3a3fe2e8	t	\N	2026-02-16 11:43:54.782014+01	\N
019c660c-b51f-7312-a2ad-928bf638cd6b	019a6de8-1bbf-7715-9eb1-0f800e19395a	546e6834-9c20-4b5f-9766-2190d9932e52	t	\N	2026-02-16 11:43:54.78201+01	\N
019c660c-b51f-731f-9ae3-d949c7120451	019a6de8-1bbf-7715-9eb1-0f800e19395a	5d821c3d-e07e-4dae-8759-30ac2976bf57	t	\N	2026-02-16 11:43:54.782016+01	\N
019c660c-b51f-732c-8be2-00fbf5a381f8	019a6de8-1bbf-7715-9eb1-0f800e19395a	8c0dac82-7234-4cfa-bc60-53e384fdee1a	t	\N	2026-02-16 11:43:54.782022+01	\N
019c660c-b51f-740b-ba71-fb29d5097d7a	019a6de8-1bbf-7715-9eb1-0f800e19395a	1a45d107-e4b8-4ef5-a8ed-e5694bcf8da5	t	\N	2026-02-16 11:43:54.782014+01	\N
019c660c-b51f-7496-ab14-4e334869251e	019a6de8-1bbf-7715-9eb1-0f800e19395a	b9b9e576-9119-443f-9f0c-6d96a8240199	t	\N	2026-02-16 11:43:54.782014+01	\N
019c660c-b51f-75af-bc57-f0a0f6e0c199	019a6de8-1bbf-7715-9eb1-0f800e19395a	50c20831-8117-42eb-a11b-75fa1e5a1607	t	\N	2026-02-16 11:43:54.782018+01	\N
019c660c-b51f-7620-babe-3a8ec7ba00b4	019a6de8-1bbf-7715-9eb1-0f800e19395a	c6a616de-c18c-4b7d-a450-7b729b58ed14	t	\N	2026-02-16 11:43:54.782017+01	\N
019c660c-b51f-762e-b8a8-44df1d3ac3fe	019a6de8-1bbf-7715-9eb1-0f800e19395a	2132a9df-1ab7-4b61-a96a-e4c3184c8f17	t	\N	2026-02-16 11:43:54.782022+01	\N
019c660c-b51f-7643-abac-27ce9e829b8d	019a6de8-1bbf-7715-9eb1-0f800e19395a	062fe4b1-4c4d-4d43-b5ec-991a61093992	t	\N	2026-02-16 11:43:54.782016+01	\N
019c660c-b51f-765c-8621-875cc92000c6	019a6de8-1bbf-7715-9eb1-0f800e19395a	8c63d7bf-fb94-4678-bab6-53a08d43ca2d	t	\N	2026-02-16 11:43:54.782012+01	\N
019c660c-b51f-76d5-8a5a-ac9ea7dc8dbd	019a6de8-1bbf-7715-9eb1-0f800e19395a	a637dbdb-8be2-4446-beaa-69c3c05aa3bf	t	\N	2026-02-16 11:43:54.782013+01	\N
019c660c-b51f-7903-8535-bdc3c57c9226	019a6de8-1bbf-7715-9eb1-0f800e19395a	291b4c66-3cb9-4a40-b21f-e7c645dc7af7	t	\N	2026-02-16 11:43:54.782023+01	\N
019c660c-b51f-7912-b086-68ad3963a351	019a6de8-1bbf-7715-9eb1-0f800e19395a	2d3979c7-2aba-405a-8df4-5d1b91da9084	t	\N	2026-02-16 11:43:54.782019+01	\N
019c660c-b51f-79db-81b0-f195959389ab	019a6de8-1bbf-7715-9eb1-0f800e19395a	03318db7-e246-4fce-a908-b9a43a95e930	t	\N	2026-02-16 11:43:54.782018+01	\N
019c660c-b51f-7a30-8ac1-b8c140877e4f	019a6de8-1bbf-7715-9eb1-0f800e19395a	0313ff3d-90c5-466c-bba1-ca94335dec65	t	\N	2026-02-16 11:43:54.782017+01	\N
019c660c-b51f-7d37-a81a-d204cefeb022	019a6de8-1bbf-7715-9eb1-0f800e19395a	19357300-48b8-4d64-a835-d695f31291e1	t	\N	2026-02-16 11:43:54.782013+01	\N
019c660c-b51f-7d9b-9324-3ceb052daecc	019a6de8-1bbf-7715-9eb1-0f800e19395a	2f42a57a-2589-4035-ae22-570e6099eeef	t	\N	2026-02-16 11:43:54.782012+01	\N
019c660c-b51f-7e11-8257-04416e2c590d	019a6de8-1bbf-7715-9eb1-0f800e19395a	544bf5b0-0d6a-424b-9f4d-c318f725ed22	t	\N	2026-02-16 11:43:54.782021+01	\N
019c660c-b51f-7e36-b6f4-28f45d2f5df5	019a6de8-1bbf-7715-9eb1-0f800e19395a	df1afe1a-8b49-4ac3-bcf3-71337b86b805	t	\N	2026-02-16 11:43:54.782013+01	\N
019c660c-b51f-7f3e-923c-6ccd12a45ecc	019a6de8-1bbf-7715-9eb1-0f800e19395a	fa9694aa-e757-423e-8c17-707f14403298	t	\N	2026-02-16 11:43:54.782013+01	\N
019c660c-b51f-7f45-b9b4-7ff240f5da18	019a6de8-1bbf-7715-9eb1-0f800e19395a	4c58d31a-0258-4fc7-abbe-4177b9f3cdbd	t	\N	2026-02-16 11:43:54.78201+01	\N
019c660c-b51f-7fb5-b606-ada8232b57cd	019a6de8-1bbf-7715-9eb1-0f800e19395a	14fdc5d2-6e87-4b38-873b-650c9bfb7707	t	\N	2026-02-16 11:43:54.782011+01	\N
019c660c-b51f-7fd5-a5f4-c212963a4990	019a6de8-1bbf-7715-9eb1-0f800e19395a	b240b3bf-ab1f-42e3-89f7-25c23f980c32	t	\N	2026-02-16 11:43:54.782015+01	\N
019c660c-b520-7185-b108-d98b2cf2043d	019a6de8-1bbf-7715-9eb1-0f800e19395a	60f2e0af-4b40-4d63-a611-cf5963f16a69	t	\N	2026-02-16 11:43:54.782026+01	\N
019c660c-b520-74cb-8827-a4523c6d472f	019a6de8-1bbf-7715-9eb1-0f800e19395a	f98bc1b0-6bf1-4e89-95b9-439cd75fdb63	t	\N	2026-02-16 11:43:54.782024+01	\N
019c660c-b520-74f9-81f3-ffe278d038fb	019a6de8-1bbf-7715-9eb1-0f800e19395a	84d6ca18-3f5c-4b73-88b0-9ab8c5766ca4	t	\N	2026-02-16 11:43:54.782025+01	\N
019c660c-b520-7adb-bc11-6382021592d0	019a6de8-1bbf-7715-9eb1-0f800e19395a	091283c5-7336-414b-b05f-e7d13d7657a3	t	\N	2026-02-16 11:43:54.782027+01	\N
019c660c-b520-7b58-b88e-b404c5613afe	019a6de8-1bbf-7715-9eb1-0f800e19395a	758c857b-bb75-4b9a-a8d3-02936ebde21c	t	\N	2026-02-16 11:43:54.782024+01	\N
019c660c-b520-7fc7-9ff4-b9edc72266a3	019a6de8-1bbf-7715-9eb1-0f800e19395a	7653f370-529e-4fd0-9790-07cacb317ebf	t	\N	2026-02-16 11:43:54.782026+01	\N
019c660d-a288-7c35-95bc-8cf9f19ed230	019c49b1-4170-779e-85d2-6a2e26582981	2f42a57a-2589-4035-ae22-570e6099eeef	t	\N	2026-02-16 11:44:55.559396+01	\N
019c660e-87b3-7134-87ea-408d7922a3f0	019c49b1-4170-779e-85d2-6a2e26582981	0313ff3d-90c5-466c-bba1-ca94335dec65	t	\N	2026-02-16 11:45:54.226122+01	\N
019c662c-53d2-774a-a696-0e55f3babd3e	019c49b1-4170-779e-85d2-6a2e26582981	df1afe1a-8b49-4ac3-bcf3-71337b86b805	f	2026-02-16 12:30:21.350797+01	2026-02-16 12:18:27.02501+01	\N
019c6689-1b2d-772b-88ac-cce7fbb439c8	019a6de8-1bbf-7715-9eb1-0f800e19395a	541bbeba-78eb-482a-a16a-8c7e217aafb4	t	\N	2026-02-16 13:59:47.355506+01	\N
019c660b-eb05-7373-b01a-1b993040fc8c	019c49b1-4170-779e-85d2-6a2e26582981	0cb9af40-616c-4a7c-b323-3d4c5bfef50b	t	\N	2026-02-16 11:43:03.000705+01	\N
019c660b-eb05-7db3-ad14-c4c64f771ee9	019c49b1-4170-779e-85d2-6a2e26582981	7c17d8be-235a-4f5b-8406-4cd3101150f0	t	\N	2026-02-16 11:43:03.000704+01	\N
019c66a1-2eaa-7058-801b-3ac4d3d80749	019c66a1-2e93-7aba-8015-968c6a165159	541bbeba-78eb-482a-a16a-8c7e217aafb4	t	\N	2026-02-16 14:26:05.192623+01	\N
019c66a1-2eaa-70b3-b38f-e5f2306681c6	019c66a1-2e93-7aba-8015-968c6a165159	06b72e90-34af-4f5c-9687-d3bf38361d80	t	\N	2026-02-16 14:26:05.192623+01	\N
019c66a1-2ea3-7303-ad8b-552a915f8be2	019c66a1-2e93-7aba-8015-968c6a165159	cc8ce820-648a-4c1b-b948-6acd2d42edbf	f	2026-02-16 15:17:31.529967+01	2026-02-16 14:26:05.192602+01	\N
019c660b-eb07-72e1-8a1d-898f3396c56e	019c49b1-4170-779e-85d2-6a2e26582981	4b8cd345-a457-4e6a-9f4d-86a3e7e25144	t	\N	2026-02-16 11:43:03.000722+01	\N
019c66a1-2eaa-70f2-a920-f704f1732839	019c66a1-2e93-7aba-8015-968c6a165159	b4c1ee72-6daa-4e57-bf48-5e4eaffc636e	t	\N	2026-02-16 14:26:05.192622+01	\N
019c66a1-2eaa-718f-8d45-a1c097533d5e	019c66a1-2e93-7aba-8015-968c6a165159	3700f441-531e-4e50-b54a-62565d74c52b	t	\N	2026-02-16 14:26:05.192623+01	\N
019c66a1-2eaa-71d0-bfe7-28d72525b26d	019c66a1-2e93-7aba-8015-968c6a165159	40237e18-dff7-48c3-a4da-f92624a3b335	t	\N	2026-02-16 14:26:05.19262+01	\N
019c66a1-2eaa-7273-a252-437aa1026564	019c66a1-2e93-7aba-8015-968c6a165159	fccc4094-0cba-411d-83da-f283ee7b5a31	t	\N	2026-02-16 14:26:05.192622+01	\N
019c66a1-2eaa-7275-aabb-1cfd6c4a0ce5	019c66a1-2e93-7aba-8015-968c6a165159	341bf0dd-e081-49af-80e6-f6baa5c14da1	t	\N	2026-02-16 14:26:05.192622+01	\N
019c66a1-2eaa-72d4-87e4-5394cf7dbee6	019c66a1-2e93-7aba-8015-968c6a165159	c8c3ebe3-36d0-4b1d-a1d8-173d1fe0c08c	t	\N	2026-02-16 14:26:05.192623+01	\N
019c66a1-2eaa-7306-b212-2b7cf729e509	019c66a1-2e93-7aba-8015-968c6a165159	a1d3e1cc-7819-4d30-bf9b-086d7f8e6669	t	\N	2026-02-16 14:26:05.192623+01	\N
019c66a1-2eaa-73db-a18e-09a5cf668a53	019c66a1-2e93-7aba-8015-968c6a165159	f26b44f5-b188-49d7-9108-ab8d0f28ca88	t	\N	2026-02-16 14:26:05.192621+01	\N
019c66a1-2eaa-7582-8b4e-fce0229eb671	019c66a1-2e93-7aba-8015-968c6a165159	9fc0ef5f-bca6-4b24-a3ac-8f5dd7a54669	t	\N	2026-02-16 14:26:05.19262+01	\N
019c66a1-2eaa-75bf-96fc-b0581e8c18a2	019c66a1-2e93-7aba-8015-968c6a165159	e8171dc1-2bbc-4249-b90c-e506f7acdea7	t	\N	2026-02-16 14:26:05.192622+01	\N
019c66a1-2eaa-7608-abe6-cc4eaa2c81d7	019c66a1-2e93-7aba-8015-968c6a165159	a0a87fd7-c2a0-4dc4-829b-e6c9bd654f93	t	\N	2026-02-16 14:26:05.192624+01	\N
019c66a1-2eaa-7611-bc4d-bce170f1253e	019c66a1-2e93-7aba-8015-968c6a165159	a4a94ce9-a1c1-451a-b221-be6ae617ea62	t	\N	2026-02-16 14:26:05.192622+01	\N
019c660c-b51f-7c1b-8dd3-9abd840064c2	019a6de8-1bbf-7715-9eb1-0f800e19395a	0cffd856-f777-45e6-8855-9895631ff787	t	\N	2026-02-16 11:43:54.782011+01	\N
019c660c-b51f-76ad-aff6-b5d17862f7f9	019a6de8-1bbf-7715-9eb1-0f800e19395a	57e53af8-de3b-486b-8560-3ad6bc1f1786	t	\N	2026-02-16 11:43:54.782009+01	\N
019c660c-b51f-773b-89c4-d0895e225639	019a6de8-1bbf-7715-9eb1-0f800e19395a	641a636b-5f28-42d1-8acb-56e5498beef2	t	\N	2026-02-16 11:43:54.782011+01	\N
019c660c-b51f-792b-8718-822e108ec923	019a6de8-1bbf-7715-9eb1-0f800e19395a	45d6a786-a896-4e13-a6b2-a7d37fd3027f	t	\N	2026-02-16 11:43:54.782012+01	\N
019c66a1-2eaa-76ac-88bd-69ec5bf3a988	019c66a1-2e93-7aba-8015-968c6a165159	5aedeb5c-5651-4b43-a364-6b16863e2b9b	t	\N	2026-02-16 14:26:05.192623+01	\N
019c66a1-2eaa-772f-abc7-8e9bab31e33e	019c66a1-2e93-7aba-8015-968c6a165159	b6a055b9-a2b2-466f-871a-e50ae7a217a0	t	\N	2026-02-16 14:26:05.19262+01	\N
019c66a1-2eaa-773c-bb41-0b3381d26db5	019c66a1-2e93-7aba-8015-968c6a165159	4e2e674c-b5e2-4a0a-b087-cdb26a10917e	t	\N	2026-02-16 14:26:05.192622+01	\N
019c66a1-2eaa-7779-8414-05364049a93c	019c66a1-2e93-7aba-8015-968c6a165159	2e8751d0-be49-4499-89b2-54f52eb6397c	t	\N	2026-02-16 14:26:05.192623+01	\N
019c66a1-2eaa-77c5-9f8c-747e1e42f613	019c66a1-2e93-7aba-8015-968c6a165159	90725bf8-58cb-48e5-ac88-18871f10a896	t	\N	2026-02-16 14:26:05.192624+01	\N
019c66a1-2eaa-77d5-817d-d0ead92825b3	019c66a1-2e93-7aba-8015-968c6a165159	e456ad34-f11c-40a2-b398-5a031ff736c5	t	\N	2026-02-16 14:26:05.19262+01	\N
019c66a1-2eaa-7836-bcb9-d1936d141cff	019c66a1-2e93-7aba-8015-968c6a165159	32eccc2e-f468-4773-b464-33fac06229d1	t	\N	2026-02-16 14:26:05.192622+01	\N
019c66a1-2eaa-7868-afce-7d31c32b5660	019c66a1-2e93-7aba-8015-968c6a165159	d75c269c-23f6-42ba-9bc5-d4a7bf8dc8b1	t	\N	2026-02-16 14:26:05.192623+01	\N
019c66a1-2eaa-7884-981a-022142b6cffb	019c66a1-2e93-7aba-8015-968c6a165159	fed66b9f-eed6-412a-9a86-74dac8485a7f	t	\N	2026-02-16 14:26:05.192624+01	\N
019c66a1-2eaa-78bc-a6f0-60fc06bb2848	019c66a1-2e93-7aba-8015-968c6a165159	176cd49a-cf34-460c-a133-1dbd49c1696a	t	\N	2026-02-16 14:26:05.19262+01	\N
019c66a1-2eaa-78d4-999f-e43306530c28	019c66a1-2e93-7aba-8015-968c6a165159	58357715-f090-420e-b647-da5b1b79ce49	t	\N	2026-02-16 14:26:05.192624+01	\N
019c66a1-2eaa-78ee-a8ef-4edebed5b8a2	019c66a1-2e93-7aba-8015-968c6a165159	06a2cb9c-bf0c-4161-88b4-7f3d272152dc	t	\N	2026-02-16 14:26:05.192623+01	\N
019c66a1-2eaa-79d8-8748-4087da7b97f5	019c66a1-2e93-7aba-8015-968c6a165159	254300e8-1473-421e-845c-0ebfa7931cfb	t	\N	2026-02-16 14:26:05.192622+01	\N
019c66a1-2eaa-79f6-8313-f152f8d36a71	019c66a1-2e93-7aba-8015-968c6a165159	9c9c5206-6551-4bfa-90ff-1713d9e1c5a0	t	\N	2026-02-16 14:26:05.192622+01	\N
019c66a1-2eaa-7a2c-8854-323642ca4807	019c66a1-2e93-7aba-8015-968c6a165159	97f39eef-6efb-473c-9f00-56630772ba7c	t	\N	2026-02-16 14:26:05.192622+01	\N
019c66a1-2eaa-7a33-971b-e5973336904d	019c66a1-2e93-7aba-8015-968c6a165159	980963f4-1b8b-45ff-b78d-3d1c027beab1	t	\N	2026-02-16 14:26:05.192621+01	\N
019c66a1-2eaa-7a5c-9200-4b8b1f174602	019c66a1-2e93-7aba-8015-968c6a165159	4e87f0e7-ee11-4df3-a667-f113585e5d8a	t	\N	2026-02-16 14:26:05.19262+01	\N
019c66a1-2eaa-7ab3-8103-260ee5e1d5e6	019c66a1-2e93-7aba-8015-968c6a165159	9e4870fc-544e-4c48-8080-f0bd01fbf86c	t	\N	2026-02-16 14:26:05.192623+01	\N
019c66a1-2eaa-7cc7-b045-2de5976475b1	019c66a1-2e93-7aba-8015-968c6a165159	5129bdb9-8ae7-4c92-9517-abb634b2d93b	t	\N	2026-02-16 14:26:05.192623+01	\N
019c66a1-2eaa-7d08-8d87-200c74b6fcee	019c66a1-2e93-7aba-8015-968c6a165159	09c97b8e-6747-41d6-a2cc-cd12eb610336	t	\N	2026-02-16 14:26:05.192623+01	\N
019c66a1-2eaa-7d4b-90ee-ff52b565b1c0	019c66a1-2e93-7aba-8015-968c6a165159	b4d92772-cff6-4172-9c57-d79b8e8d3f9d	t	\N	2026-02-16 14:26:05.19262+01	\N
019c66a1-2eaa-7d62-a9d4-7874a815e994	019c66a1-2e93-7aba-8015-968c6a165159	a9a0de3b-17a2-4bb4-8cd3-51670eef023b	t	\N	2026-02-16 14:26:05.192623+01	\N
019c66a1-2eaa-7ed6-9a3f-72cfa25c4c80	019c66a1-2e93-7aba-8015-968c6a165159	13f7b8bd-e881-47c0-bf4a-fe21a472c1ae	t	\N	2026-02-16 14:26:05.192624+01	\N
019c66a1-2eaa-7f37-a249-9b922df7337a	019c66a1-2e93-7aba-8015-968c6a165159	c6298f91-4231-4bda-828f-d71ca896cd36	t	\N	2026-02-16 14:26:05.19262+01	\N
019c66a1-2eaa-7f77-971e-b79cbc15d91f	019c66a1-2e93-7aba-8015-968c6a165159	19ee9066-c3fb-474e-abb1-38306f73b23a	t	\N	2026-02-16 14:26:05.19262+01	\N
019c66a1-2eab-7068-969e-c8b9c4455a41	019c66a1-2e93-7aba-8015-968c6a165159	daef4083-2b1b-42c1-8050-a19cb2febfc6	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eab-7188-b096-d4bd98a6d119	019c66a1-2e93-7aba-8015-968c6a165159	b931c4e0-be92-467c-b3cb-e52204e2fa39	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eab-71d4-9499-7e1ee61d00f4	019c66a1-2e93-7aba-8015-968c6a165159	1b7a8176-c19b-42e1-841a-6a635059393e	t	\N	2026-02-16 14:26:05.192624+01	\N
019c66a1-2eab-72e1-b73f-4bc9dc05b9e9	019c66a1-2e93-7aba-8015-968c6a165159	0cb9af40-616c-4a7c-b323-3d4c5bfef50b	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eab-7354-9eb9-9c0f5dd7e32c	019c66a1-2e93-7aba-8015-968c6a165159	8a6fd0d6-1baf-497d-9f47-99970aa0ef5a	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eab-7403-bfa0-7b633b9df15c	019c66a1-2e93-7aba-8015-968c6a165159	5b8aebac-e7a6-4911-938f-f19f1d450927	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eab-743c-997f-740aad3d7ccb	019c66a1-2e93-7aba-8015-968c6a165159	fa5e1d27-ee19-4551-847c-65dc3a560b0d	t	\N	2026-02-16 14:26:05.192624+01	\N
019c66a1-2eab-764a-8e80-9247b4ff0d27	019c66a1-2e93-7aba-8015-968c6a165159	6ca9c36e-3ea7-4275-8e46-6aeb87e096e4	t	\N	2026-02-16 14:26:05.192624+01	\N
019c66a1-2eab-76d2-b93b-0b9c7eaead3b	019c66a1-2e93-7aba-8015-968c6a165159	8e8b5b0f-7d95-455c-8be0-bf9cf73f4561	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eab-7705-844d-db287f2e45d3	019c66a1-2e93-7aba-8015-968c6a165159	2b434e2e-b17c-4293-8e13-6a227864653c	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eab-7706-ac4b-8193556e53d4	019c66a1-2e93-7aba-8015-968c6a165159	4ceee8dd-5e9d-4649-b589-4781d8f17160	t	\N	2026-02-16 14:26:05.192624+01	\N
019c66a1-2eab-7890-9e71-f9dc4e847b6f	019c66a1-2e93-7aba-8015-968c6a165159	3562efa0-d655-4ece-9692-130feaf98124	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eab-7896-b3fd-428419871ef2	019c66a1-2e93-7aba-8015-968c6a165159	dfe048c0-4cd8-4cda-8c1a-68ce32419ea8	t	\N	2026-02-16 14:26:05.192624+01	\N
019c66a1-2eab-790a-9516-6ae9feba969c	019c66a1-2e93-7aba-8015-968c6a165159	5f22fc2b-e663-45f1-bc15-cfb5c136f280	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eab-79e5-b520-98d24bad37bf	019c66a1-2e93-7aba-8015-968c6a165159	0c2b88ff-cd57-4504-8124-4c76cfe73b1e	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eab-7b0a-b6ec-0d631afe0882	019c66a1-2e93-7aba-8015-968c6a165159	115e8d0f-3266-4c39-b5fb-1d88c5fa8af7	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eab-7ba1-86e0-a058bb4c4dff	019c66a1-2e93-7aba-8015-968c6a165159	dfb4541d-6d04-4976-a6ca-1163a79dab8d	t	\N	2026-02-16 14:26:05.192624+01	\N
019c66a1-2eab-7bac-b024-ec7044d8dc52	019c66a1-2e93-7aba-8015-968c6a165159	df8eb3c8-92d4-47ed-b1ef-d0220f04f272	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eab-7d67-9387-cc4d31b1d241	019c66a1-2e93-7aba-8015-968c6a165159	85b411b6-cf5b-4ebc-b69f-61a65e88bccd	t	\N	2026-02-16 14:26:05.192624+01	\N
019c66a1-2eab-7d9c-95c2-37683eddb072	019c66a1-2e93-7aba-8015-968c6a165159	17387ae0-43ef-45c4-879f-99ea44f0a727	t	\N	2026-02-16 14:26:05.192624+01	\N
019c66a1-2eab-7da9-88b8-7cc52d6609c1	019c66a1-2e93-7aba-8015-968c6a165159	917bb52b-e16e-4e09-aafb-65dc7571d543	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eab-7dd6-bcf4-b7764844c446	019c66a1-2e93-7aba-8015-968c6a165159	5554b3c8-e88e-4a88-b4b8-14a601f0d3e5	t	\N	2026-02-16 14:26:05.192624+01	\N
019c66a1-2eab-7e0b-9c97-dc0c613ca889	019c66a1-2e93-7aba-8015-968c6a165159	bf815458-7960-4dce-912a-67df8d350f1d	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eab-7f2c-911f-2da84fab6118	019c66a1-2e93-7aba-8015-968c6a165159	7c17d8be-235a-4f5b-8406-4cd3101150f0	t	\N	2026-02-16 14:26:05.192625+01	\N
019c66a1-2eac-72d0-870f-0afe49808dfe	019c66a1-2e93-7aba-8015-968c6a165159	eedf6023-1dda-430a-aa63-84b239217c92	t	\N	2026-02-16 14:26:05.192626+01	\N
019c66a1-2eac-72e0-946b-d8264561e3f4	019c66a1-2e93-7aba-8015-968c6a165159	927398e9-d97e-4491-9834-1c7dad4fc6e7	t	\N	2026-02-16 14:26:05.192627+01	\N
019c66a1-2eac-72fb-94ad-00c51a3a1bd7	019c66a1-2e93-7aba-8015-968c6a165159	ae45f4f8-15c4-431d-86de-33c41991fe9f	t	\N	2026-02-16 14:26:05.192627+01	\N
019c66a1-2eac-7439-ad8a-a74427193e77	019c66a1-2e93-7aba-8015-968c6a165159	46294206-dcfc-45be-ad5a-35794fcf216b	t	\N	2026-02-16 14:26:05.192626+01	\N
019c66a1-2eac-7470-9ebf-83f691f8c16a	019c66a1-2e93-7aba-8015-968c6a165159	d2d71325-d06a-4af1-827b-258a88fed1f2	t	\N	2026-02-16 14:26:05.192627+01	\N
019c66a1-2eac-7518-afe2-3b3c5f9e7b84	019c66a1-2e93-7aba-8015-968c6a165159	1bffb46e-64ff-40f9-8f22-e65dd9d607e7	t	\N	2026-02-16 14:26:05.192627+01	\N
019c66a1-2eac-7560-b326-48ec01f19f33	019c66a1-2e93-7aba-8015-968c6a165159	4b8cd345-a457-4e6a-9f4d-86a3e7e25144	t	\N	2026-02-16 14:26:05.192627+01	\N
019c66a1-2eac-7730-afe8-6cc5044c4d4d	019c66a1-2e93-7aba-8015-968c6a165159	318324ef-63b3-4461-8f05-a9d9646d58cf	t	\N	2026-02-16 14:26:05.192627+01	\N
019c66a1-2eac-7730-b0e1-703151533e34	019c66a1-2e93-7aba-8015-968c6a165159	9d53ed40-2147-48a0-aada-d06c9aa542dd	t	\N	2026-02-16 14:26:05.192626+01	\N
019c66a1-2eac-775b-89e9-b89cf1c70b3b	019c66a1-2e93-7aba-8015-968c6a165159	2f42a57a-2589-4035-ae22-570e6099eeef	t	\N	2026-02-16 14:26:05.192627+01	\N
019c66a1-2eac-78d2-b7b6-3fa25b800372	019c66a1-2e93-7aba-8015-968c6a165159	0313ff3d-90c5-466c-bba1-ca94335dec65	t	\N	2026-02-16 14:26:05.192627+01	\N
019c66a1-2eac-7a14-b300-51eae32fb483	019c66a1-2e93-7aba-8015-968c6a165159	675ae22a-3a1e-42d7-b731-1ed0c90afe53	t	\N	2026-02-16 14:26:05.192626+01	\N
019c66a1-2eac-7c02-a30d-5f5a0a1e8365	019c66a1-2e93-7aba-8015-968c6a165159	590ed689-3fed-41fd-b913-a92cae299391	t	\N	2026-02-16 14:26:05.192627+01	\N
019c66a1-2eac-7c5f-9400-33164072a98f	019c66a1-2e93-7aba-8015-968c6a165159	a7b5b2c7-77b6-4d0d-a177-ee640edf5c61	t	\N	2026-02-16 14:26:05.192627+01	\N
019c66a1-2eac-7cc5-9852-9805ecda16a2	019c66a1-2e93-7aba-8015-968c6a165159	4d7c6048-482f-45eb-b163-1b515178a016	t	\N	2026-02-16 14:26:05.192627+01	\N
019c66a1-2eac-7e7d-a55b-8a9f19594f63	019c66a1-2e93-7aba-8015-968c6a165159	e0420f2b-8677-468d-abff-c65fa4224cf8	t	\N	2026-02-16 14:26:05.192627+01	\N
019c66a1-2eaa-7d3b-845a-df17d74aed15	019c66a1-2e93-7aba-8015-968c6a165159	7f179e31-4197-4a21-bda8-f24a6e4bd40e	f	2026-02-16 15:17:31.530009+01	2026-02-16 14:26:05.192619+01	\N
019c670a-7980-756a-a994-121b7587e249	019b0e2f-9047-748c-b1d1-13fff048abcd	d2d71325-d06a-4af1-827b-258a88fed1f2	t	\N	2026-02-16 16:21:05.644988+01	\N
019c670a-7980-7730-ae30-b7f473b60770	019b0e2f-9047-748c-b1d1-13fff048abcd	590ed689-3fed-41fd-b913-a92cae299391	t	\N	2026-02-16 16:21:05.644988+01	\N
019c670a-7980-7737-978e-657ac7495937	019b0e2f-9047-748c-b1d1-13fff048abcd	fed66b9f-eed6-412a-9a86-74dac8485a7f	t	\N	2026-02-16 16:21:05.644983+01	\N
019c670a-7980-7a36-84e3-55e9d4d9c0cb	019b0e2f-9047-748c-b1d1-13fff048abcd	13f7b8bd-e881-47c0-bf4a-fe21a472c1ae	t	\N	2026-02-16 16:21:05.644984+01	\N
019c670a-7980-7a67-8502-40a6fc36bd3e	019b0e2f-9047-748c-b1d1-13fff048abcd	318324ef-63b3-4461-8f05-a9d9646d58cf	t	\N	2026-02-16 16:21:05.644988+01	\N
019c670a-7980-7c88-91ea-9786601af020	019b0e2f-9047-748c-b1d1-13fff048abcd	4d7c6048-482f-45eb-b163-1b515178a016	t	\N	2026-02-16 16:21:05.644988+01	\N
019c670a-7980-7ce5-9691-0f02fe70c597	019b0e2f-9047-748c-b1d1-13fff048abcd	90725bf8-58cb-48e5-ac88-18871f10a896	t	\N	2026-02-16 16:21:05.644984+01	\N
019c670a-7980-7e3e-85fc-e523c3a7ff00	019b0e2f-9047-748c-b1d1-13fff048abcd	fa5e1d27-ee19-4551-847c-65dc3a560b0d	t	\N	2026-02-16 16:21:05.644985+01	\N
019c670a-7980-7f1d-ad4c-00293444a777	019b0e2f-9047-748c-b1d1-13fff048abcd	a0a87fd7-c2a0-4dc4-829b-e6c9bd654f93	t	\N	2026-02-16 16:21:05.644984+01	\N
019c670a-7980-7f20-a4dd-08bb3998db8c	019b0e2f-9047-748c-b1d1-13fff048abcd	dfe048c0-4cd8-4cda-8c1a-68ce32419ea8	t	\N	2026-02-16 16:21:05.644986+01	\N
019c670a-7978-79ed-9296-5016356472e2	019b0e2f-9047-748c-b1d1-13fff048abcd	541bbeba-78eb-482a-a16a-8c7e217aafb4	f	2026-02-16 16:27:44.100549+01	2026-02-16 16:21:05.644866+01	\N
019c670a-7980-73d2-ac6d-3d9f738a8290	019b0e2f-9047-748c-b1d1-13fff048abcd	bf815458-7960-4dce-912a-67df8d350f1d	f	2026-02-16 16:27:44.100604+01	2026-02-16 16:21:05.644987+01	\N
019c670a-7980-7703-a5ed-9cdfdeeba1f8	019b0e2f-9047-748c-b1d1-13fff048abcd	4b8cd345-a457-4e6a-9f4d-86a3e7e25144	t	\N	2026-02-16 16:21:05.644989+01	\N
019c670a-7980-73e6-9243-60aabf57036b	019b0e2f-9047-748c-b1d1-13fff048abcd	ae45f4f8-15c4-431d-86de-33c41991fe9f	f	2026-02-16 16:27:44.100604+01	2026-02-16 16:21:05.644988+01	\N
019c670a-7980-7426-9a88-c088c1fe74c1	019b0e2f-9047-748c-b1d1-13fff048abcd	8a6fd0d6-1baf-497d-9f47-99970aa0ef5a	f	2026-02-16 16:27:44.100604+01	2026-02-16 16:21:05.644986+01	\N
019c670a-7980-751e-92f4-ffa59fd76684	019b0e2f-9047-748c-b1d1-13fff048abcd	5b8aebac-e7a6-4911-938f-f19f1d450927	f	2026-02-16 16:27:44.100604+01	2026-02-16 16:21:05.644987+01	\N
019c670a-7980-751f-ad5a-9a5452221b84	019b0e2f-9047-748c-b1d1-13fff048abcd	4ceee8dd-5e9d-4649-b589-4781d8f17160	f	2026-02-16 16:27:44.100605+01	2026-02-16 16:21:05.644986+01	\N
019c670a-7980-7756-b59c-717ef559baf0	019b0e2f-9047-748c-b1d1-13fff048abcd	daef4083-2b1b-42c1-8050-a19cb2febfc6	f	2026-02-16 16:27:44.100605+01	2026-02-16 16:21:05.644987+01	\N
019c670a-7980-7caa-bb40-66dcab7f19aa	019b0e2f-9047-748c-b1d1-13fff048abcd	0c2b88ff-cd57-4504-8124-4c76cfe73b1e	f	2026-02-16 16:27:44.100606+01	2026-02-16 16:21:05.644987+01	\N
019c6710-8de5-7004-ac99-d10330bc91a2	019b0e2f-9047-748c-b1d1-13fff048abcd	e8171dc1-2bbc-4249-b90c-e506f7acdea7	t	\N	2026-02-16 16:27:44.100611+01	\N
019c6710-8de5-7478-a0a1-b669b2459cd4	019b0e2f-9047-748c-b1d1-13fff048abcd	980963f4-1b8b-45ff-b78d-3d1c027beab1	t	\N	2026-02-16 16:27:44.10061+01	\N
019c6710-8de5-74c9-b71e-e9e23914bd91	019b0e2f-9047-748c-b1d1-13fff048abcd	9e4870fc-544e-4c48-8080-f0bd01fbf86c	t	\N	2026-02-16 16:27:44.100612+01	\N
019c6710-8de5-76e8-b8f4-ad9391fb3cf5	019b0e2f-9047-748c-b1d1-13fff048abcd	a4a94ce9-a1c1-451a-b221-be6ae617ea62	t	\N	2026-02-16 16:27:44.100611+01	\N
019c6710-8de5-78d4-abb7-954571300a1b	019b0e2f-9047-748c-b1d1-13fff048abcd	9fc0ef5f-bca6-4b24-a3ac-8f5dd7a54669	t	\N	2026-02-16 16:27:44.100609+01	\N
019c6710-8de5-7dce-947e-ea71d0e4a373	019b0e2f-9047-748c-b1d1-13fff048abcd	06b72e90-34af-4f5c-9687-d3bf38361d80	t	\N	2026-02-16 16:27:44.100612+01	\N
019c670a-7980-7c43-aae9-d476e99b4431	019b0e2f-9047-748c-b1d1-13fff048abcd	5f22fc2b-e663-45f1-bc15-cfb5c136f280	t	\N	2026-02-16 16:21:05.644987+01	\N
019c671a-1c72-7a6c-b976-fb17d0f26d94	019b0e2f-9047-748c-b1d1-13fff048abcd	2f42a57a-2589-4035-ae22-570e6099eeef	t	\N	2026-02-16 16:38:10.41774+01	\N
019c6723-6ac4-7019-bae9-04f86b93d6fb	019b0e2f-9047-748c-b1d1-13fff048abcd	2b434e2e-b17c-4293-8e13-6a227864653c	t	\N	2026-02-16 16:48:20.291644+01	\N
019c6727-f863-701d-b5aa-e8d15a7ce04d	019c6727-f863-7e7f-8a49-14d79265d614	9fc0ef5f-bca6-4b24-a3ac-8f5dd7a54669	t	\N	2026-02-16 16:53:18.687204+01	\N
019c6727-f863-705f-8e3e-73b214f6ad56	019c6727-f863-7e7f-8a49-14d79265d614	e8171dc1-2bbc-4249-b90c-e506f7acdea7	t	\N	2026-02-16 16:53:18.687207+01	\N
019c6727-f863-7064-b6f8-3bb62768a1b3	019c6727-f863-7e7f-8a49-14d79265d614	590ed689-3fed-41fd-b913-a92cae299391	t	\N	2026-02-16 16:53:18.687211+01	\N
019c6727-f863-724b-8802-47a15af1c023	019c6727-f863-7e7f-8a49-14d79265d614	318324ef-63b3-4461-8f05-a9d9646d58cf	t	\N	2026-02-16 16:53:18.687211+01	\N
019c6727-f863-72e5-9d71-055f70b542ea	019c6727-f863-7e7f-8a49-14d79265d614	9e4870fc-544e-4c48-8080-f0bd01fbf86c	t	\N	2026-02-16 16:53:18.687207+01	\N
019c6727-f863-7395-a44a-74404b6ef360	019c6727-f863-7e7f-8a49-14d79265d614	bf815458-7960-4dce-912a-67df8d350f1d	t	\N	2026-02-16 16:53:18.68721+01	\N
019c6727-f863-73f2-96c3-bdaa96b39cdb	019c6727-f863-7e7f-8a49-14d79265d614	daef4083-2b1b-42c1-8050-a19cb2febfc6	t	\N	2026-02-16 16:53:18.68721+01	\N
019c6727-f863-74da-a580-9119fd8db4c2	019c6727-f863-7e7f-8a49-14d79265d614	a4a94ce9-a1c1-451a-b221-be6ae617ea62	t	\N	2026-02-16 16:53:18.687207+01	\N
019c6727-f863-7552-863c-383261fb95da	019c6727-f863-7e7f-8a49-14d79265d614	1bffb46e-64ff-40f9-8f22-e65dd9d607e7	t	\N	2026-02-16 16:53:18.687211+01	\N
019c6727-f863-75ba-ab51-5bc997da6ed3	019c6727-f863-7e7f-8a49-14d79265d614	0c2b88ff-cd57-4504-8124-4c76cfe73b1e	t	\N	2026-02-16 16:53:18.68721+01	\N
019c6727-f863-75ec-9d2f-942f71cab9fb	019c6727-f863-7e7f-8a49-14d79265d614	917bb52b-e16e-4e09-aafb-65dc7571d543	t	\N	2026-02-16 16:53:18.687211+01	\N
019c6727-f863-7664-ae72-feef5fab683e	019c6727-f863-7e7f-8a49-14d79265d614	a7b5b2c7-77b6-4d0d-a177-ee640edf5c61	t	\N	2026-02-16 16:53:18.687211+01	\N
019c6727-f863-7666-b38a-7459f281cc4d	019c6727-f863-7e7f-8a49-14d79265d614	5f22fc2b-e663-45f1-bc15-cfb5c136f280	t	\N	2026-02-16 16:53:18.68721+01	\N
019c6727-f863-76b4-a500-887fc7cf38c9	019c6727-f863-7e7f-8a49-14d79265d614	8a6fd0d6-1baf-497d-9f47-99970aa0ef5a	t	\N	2026-02-16 16:53:18.687209+01	\N
019c6727-f863-76b5-87e7-ae30b734ae52	019c6727-f863-7e7f-8a49-14d79265d614	d2d71325-d06a-4af1-827b-258a88fed1f2	t	\N	2026-02-16 16:53:18.687211+01	\N
019c6727-f863-76ec-806b-78fb356776f8	019c6727-f863-7e7f-8a49-14d79265d614	fa5e1d27-ee19-4551-847c-65dc3a560b0d	t	\N	2026-02-16 16:53:18.687209+01	\N
019c6727-f863-772b-8039-62e5b9c3f07b	019c6727-f863-7e7f-8a49-14d79265d614	2b434e2e-b17c-4293-8e13-6a227864653c	t	\N	2026-02-16 16:53:18.687209+01	\N
019c6727-f863-778a-bec5-f39023c4615c	019c6727-f863-7e7f-8a49-14d79265d614	e0420f2b-8677-468d-abff-c65fa4224cf8	t	\N	2026-02-16 16:53:18.687211+01	\N
019c6727-f863-77c7-b373-d01064411af1	019c6727-f863-7e7f-8a49-14d79265d614	2f42a57a-2589-4035-ae22-570e6099eeef	t	\N	2026-02-16 16:53:18.687211+01	\N
019c6727-f863-7876-852e-cd9f1c1f1f26	019c6727-f863-7e7f-8a49-14d79265d614	06b72e90-34af-4f5c-9687-d3bf38361d80	t	\N	2026-02-16 16:53:18.687208+01	\N
019c6727-f863-7a28-975f-291c47712432	019c6727-f863-7e7f-8a49-14d79265d614	927398e9-d97e-4491-9834-1c7dad4fc6e7	t	\N	2026-02-16 16:53:18.687211+01	\N
019c6727-f863-7c87-a8a0-a59ecb607ad1	019c6727-f863-7e7f-8a49-14d79265d614	980963f4-1b8b-45ff-b78d-3d1c027beab1	t	\N	2026-02-16 16:53:18.687207+01	\N
019c6727-f863-7d9c-8180-447dc0e02c9f	019c6727-f863-7e7f-8a49-14d79265d614	5b8aebac-e7a6-4911-938f-f19f1d450927	t	\N	2026-02-16 16:53:18.68721+01	\N
019c6727-f863-7df1-b6f6-1f42cec7cc16	019c6727-f863-7e7f-8a49-14d79265d614	4d7c6048-482f-45eb-b163-1b515178a016	t	\N	2026-02-16 16:53:18.687211+01	\N
019c6727-f863-7f0a-ad30-4b1cba67802c	019c6727-f863-7e7f-8a49-14d79265d614	df1afe1a-8b49-4ac3-bcf3-71337b86b805	t	\N	2026-02-16 16:53:18.687211+01	\N
019c6727-f863-70ad-9402-bcb54312b671	019c6727-f863-7e7f-8a49-14d79265d614	13f7b8bd-e881-47c0-bf4a-fe21a472c1ae	f	2026-02-21 17:05:33.058897+01	2026-02-16 16:53:18.687209+01	\N
019c6727-f863-7163-936c-9b917f87fe0d	019c6727-f863-7e7f-8a49-14d79265d614	90725bf8-58cb-48e5-ac88-18871f10a896	f	2026-02-21 17:05:33.058897+01	2026-02-16 16:53:18.687209+01	\N
019c6727-f863-7549-b76e-f63b5f7e22fe	019c6727-f863-7e7f-8a49-14d79265d614	fed66b9f-eed6-412a-9a86-74dac8485a7f	f	2026-02-21 17:05:33.058898+01	2026-02-16 16:53:18.687208+01	\N
019c6727-f863-7bc2-9cfa-e99f8cc2ca83	019c6727-f863-7e7f-8a49-14d79265d614	a0a87fd7-c2a0-4dc4-829b-e6c9bd654f93	f	2026-02-21 17:05:33.058898+01	2026-02-16 16:53:18.687209+01	\N
019c80f2-f904-7050-a0cd-33300b56d1a5	019c6727-f863-7e7f-8a49-14d79265d614	85b411b6-cf5b-4ebc-b69f-61a65e88bccd	t	\N	2026-02-21 17:05:33.058905+01	\N
019c80f2-f904-7265-acf8-d7b4bf024dcf	019c6727-f863-7e7f-8a49-14d79265d614	dfe048c0-4cd8-4cda-8c1a-68ce32419ea8	t	\N	2026-02-21 17:05:33.058904+01	\N
019c80f2-f904-72c9-8868-817a464c0eb6	019c6727-f863-7e7f-8a49-14d79265d614	17387ae0-43ef-45c4-879f-99ea44f0a727	t	\N	2026-02-21 17:05:33.058906+01	\N
019c80f2-f904-7c68-88c4-b9f0c633647d	019c6727-f863-7e7f-8a49-14d79265d614	dfb4541d-6d04-4976-a6ca-1163a79dab8d	t	\N	2026-02-21 17:05:33.058905+01	\N
019c80f7-93c9-7926-a50a-69d3dea8c4cf	019c6727-f863-7e7f-8a49-14d79265d614	58357715-f090-420e-b647-da5b1b79ce49	t	\N	2026-02-21 17:10:34.824165+01	\N
019c6721-6841-7f66-ba11-b33b42c774a7	019b0e2f-9047-748c-b1d1-13fff048abcd	58357715-f090-420e-b647-da5b1b79ce49	t	\N	2026-02-16 16:46:08.576563+01	\N
019c80e9-243e-7511-a4a9-e21f665617c5	019b0e2f-9047-748c-b1d1-13fff048abcd	0313ff3d-90c5-466c-bba1-ca94335dec65	t	\N	2026-02-21 16:54:48.753275+01	\N
\.


--
-- TOC entry 5284 (class 0 OID 26974)
-- Dependencies: 249
-- Data for Name: SaleCartOffers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."SaleCartOffers" ("Id") FROM stdin;
\.


--
-- TOC entry 5285 (class 0 OID 26977)
-- Dependencies: 250
-- Data for Name: SaleOffers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."SaleOffers" ("Id", "PricePerItem_Amount", "PricePerItem_Currency") FROM stdin;
019c80f3-d111-7deb-a77a-d95cd7b0fad5	399.000	PLN
019c80fc-9d42-7940-8279-58ec0d534495	199.000	PLN
\.


--
-- TOC entry 5286 (class 0 OID 26980)
-- Dependencies: 251
-- Data for Name: ShippingAddresses; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ShippingAddresses" ("Id", "Street", "HouseNumber", "PostalCity", "PostalCode", "UserId", "CountryId", "IsActive", "DateDeleted", "DateCreated", "DateEdited", "City", "FlatNumber", "Label", "IsDefault") FROM stdin;
019c80fb-7961-7018-bf3b-59a03cb30cb5	Siedlce	78	Korzenna	33-322	280fa9bd-1d64-4f59-bb91-c6a539cdefba	019aac16-4916-74ed-ba2f-3be44a87ac0f	t	\N	2026-02-21 17:14:50.207291+01	\N	Siedlce		Grandma	t
\.


--
-- TOC entry 5287 (class 0 OID 26987)
-- Dependencies: 252
-- Data for Name: UserPermissions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."UserPermissions" ("Id", "UserId", "PermissionId", "IsGranted", "IsActive", "DateDeleted", "DateCreated", "DateEdited") FROM stdin;
8e8b5b0f-7d95-455c-8be0-bf9cf73f4561	bea1f25c-46f5-4e94-9bed-9697d6dd078c	8e8b5b0f-7d95-455c-8be0-bf9cf73f4561	t	t	\N	2026-12-12 00:00:00+01	\N
bea1f25c-46f5-4e94-9bed-9697d6dd078c	bea1f25c-46f5-4e94-9bed-9697d6dd078c	675ae22a-3a1e-42d7-b731-1ed0c90afe53	t	t	\N	2026-12-12 00:00:00+01	\N
20b01797-ed57-4be1-be36-f29b7e2533bc	bea1f25c-46f5-4e94-9bed-9697d6dd078c	9d53ed40-2147-48a0-aada-d06c9aa542dd	t	t	\N	2026-12-12 00:00:00+01	\N
e51dfa4d-92db-491f-904d-8099faf5c384	bea1f25c-46f5-4e94-9bed-9697d6dd078c	46294206-dcfc-45be-ad5a-35794fcf216b	t	t	\N	2026-12-12 00:00:00+01	\N
16986513-8498-479e-818f-463452635e5a	bea1f25c-46f5-4e94-9bed-9697d6dd078c	eedf6023-1dda-430a-aa63-84b239217c92	t	t	\N	2026-12-12 00:00:00+01	\N
5ea6f77d-aee8-475a-8a26-abb0410feb04	bea1f25c-46f5-4e94-9bed-9697d6dd078c	ae45f4f8-15c4-431d-86de-33c41991fe9f	t	t	\N	2026-12-12 00:00:00+01	\N
cda0ace0-2dc9-448f-ab67-2e2f529b5209	bea1f25c-46f5-4e94-9bed-9697d6dd078c	7c17d8be-235a-4f5b-8406-4cd3101150f0	t	t	\N	2026-12-12 00:00:00+01	\N
\.


--
-- TOC entry 5288 (class 0 OID 26990)
-- Dependencies: 253
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20251104141536_AddIdentityAndAuthEntities	9.0.10
20251106164524_BasicEntitiesMigration	9.0.10
20251107130427_EmployeeUserMigration	9.0.10
20251107133855_RentOrderItemCorrectionMigration	9.0.10
20251110100047_PaymentIntegrationMigration	9.0.10
20251111170605_CentralizeCompanyInfoMigration	9.0.10
20251113111010_IdentityCorrectionMigration	9.0.10
20251117180214_ApplicationUserRoleCorrection	9.0.10
20251119121722_ConditionCorrectionMigration	9.0.10
20251119130726_DatabseTableNamesCorrectionMigration	9.0.10
20251119170008_SetRequirementOnMoneyValueObjectMigration	9.0.10
20251119171122_DeliveryNameMaxLengthChangeMigration	9.0.10
20251204122823_DefaultAddressMigration	9.0.10
20251212121731_OfferAndItemsChangesMigration	9.0.10
20251212130322_ImageAltTextMaxLengthChangeMigration	9.0.10
20251217135849_DeliveryChannelAndParcelSizeAddMigration	9.0.10
20251217214432_DeliveryCarrierTableAddMigration	9.0.10
20251217230554_DeliveryCarrierCorrectionMigration	9.0.10
20251226102328_AddressUserIdRequiredMigration	9.0.10
20260102155604_HomeAndBillingAddressMigration	9.0.10
20260102170301_OfferAddressSnapshotMigration	9.0.10
20260107115546_CartAggregateSeparationMigration	9.0.10
20260108170859_RentalDaysInRentCartOfferMigration	9.0.10
20260116140033_EmployeeAsPartOfCompanyMigration	9.0.10
20260120224137_ImageAltTextNullableMigration	9.0.10
20260120224311_ImageAltTextNullable2Migration	9.0.10
20260126181918_OrdersAndRentalsMigration	9.0.10
20260126185151_OrderDeliveryOptimizationMigration	9.0.10
20260129000551_OrderGroupRemoval	9.0.10
20260129001522_OrderGroupDeletion2	9.0.10
20260129002324_LastTwoMigrationsCorrection	9.0.10
20260129154659_PaymentDetailsMigration	9.0.10
20260129230752_RedundantSellerInfoOrderRemoval	9.0.10
20260130142425_OrderDeliveryCorrectionMigrtion	9.0.10
20260130151036_OrderAggregateEnforcementMigration	9.0.10
20260130214252_OrderDDDCorectionMigration	9.0.10
20260202202150_PaymentDetailsSoftDeleteMigration	9.0.10
20260203114354_OrderBuyerSnapshotMigration	9.0.10
20260205120947_RentalAggregateMigration	9.0.10
20260205170636_RentalCorrectionMigration	9.0.10
20260208153816_OfferStatusMigration	9.0.10
20260209171449_OrderLineOfferIdMigration	9.0.10
20260216090502_SystemRoleMigration	9.0.10
20260216133049_PermissionMandatoryDescriptionMigration	9.0.10
20260218235159_ReduceColumCharsPaymentDetailsMigration	9.0.10
\.


--
-- TOC entry 5325 (class 0 OID 0)
-- Dependencies: 274
-- Name: aggregatedcounter_id_seq; Type: SEQUENCE SET; Schema: hangfire; Owner: postgres
--

SELECT pg_catalog.setval('hangfire.aggregatedcounter_id_seq', 45, true);


--
-- TOC entry 5326 (class 0 OID 0)
-- Dependencies: 256
-- Name: counter_id_seq; Type: SEQUENCE SET; Schema: hangfire; Owner: postgres
--

SELECT pg_catalog.setval('hangfire.counter_id_seq', 45, true);


--
-- TOC entry 5327 (class 0 OID 0)
-- Dependencies: 258
-- Name: hash_id_seq; Type: SEQUENCE SET; Schema: hangfire; Owner: postgres
--

SELECT pg_catalog.setval('hangfire.hash_id_seq', 9, true);


--
-- TOC entry 5328 (class 0 OID 0)
-- Dependencies: 260
-- Name: job_id_seq; Type: SEQUENCE SET; Schema: hangfire; Owner: postgres
--

SELECT pg_catalog.setval('hangfire.job_id_seq', 15, true);


--
-- TOC entry 5329 (class 0 OID 0)
-- Dependencies: 271
-- Name: jobparameter_id_seq; Type: SEQUENCE SET; Schema: hangfire; Owner: postgres
--

SELECT pg_catalog.setval('hangfire.jobparameter_id_seq', 60, true);


--
-- TOC entry 5330 (class 0 OID 0)
-- Dependencies: 264
-- Name: jobqueue_id_seq; Type: SEQUENCE SET; Schema: hangfire; Owner: postgres
--

SELECT pg_catalog.setval('hangfire.jobqueue_id_seq', 15, true);


--
-- TOC entry 5331 (class 0 OID 0)
-- Dependencies: 266
-- Name: list_id_seq; Type: SEQUENCE SET; Schema: hangfire; Owner: postgres
--

SELECT pg_catalog.setval('hangfire.list_id_seq', 1, false);


--
-- TOC entry 5332 (class 0 OID 0)
-- Dependencies: 269
-- Name: set_id_seq; Type: SEQUENCE SET; Schema: hangfire; Owner: postgres
--

SELECT pg_catalog.setval('hangfire.set_id_seq', 16, true);


--
-- TOC entry 5333 (class 0 OID 0)
-- Dependencies: 262
-- Name: state_id_seq; Type: SEQUENCE SET; Schema: hangfire; Owner: postgres
--

SELECT pg_catalog.setval('hangfire.state_id_seq', 45, true);


--
-- TOC entry 5334 (class 0 OID 0)
-- Dependencies: 219
-- Name: AspNetRoleClaims_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."AspNetRoleClaims_Id_seq"', 1, false);


--
-- TOC entry 5335 (class 0 OID 0)
-- Dependencies: 222
-- Name: AspNetUserClaims_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."AspNetUserClaims_Id_seq"', 1, false);


--
-- TOC entry 5068 (class 2606 OID 27683)
-- Name: aggregatedcounter aggregatedcounter_key_key; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.aggregatedcounter
    ADD CONSTRAINT aggregatedcounter_key_key UNIQUE (key);


--
-- TOC entry 5070 (class 2606 OID 27681)
-- Name: aggregatedcounter aggregatedcounter_pkey; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.aggregatedcounter
    ADD CONSTRAINT aggregatedcounter_pkey PRIMARY KEY (id);


--
-- TOC entry 5030 (class 2606 OID 27517)
-- Name: counter counter_pkey; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.counter
    ADD CONSTRAINT counter_pkey PRIMARY KEY (id);


--
-- TOC entry 5034 (class 2606 OID 27652)
-- Name: hash hash_key_field_key; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.hash
    ADD CONSTRAINT hash_key_field_key UNIQUE (key, field);


--
-- TOC entry 5036 (class 2606 OID 27526)
-- Name: hash hash_pkey; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.hash
    ADD CONSTRAINT hash_pkey PRIMARY KEY (id);


--
-- TOC entry 5042 (class 2606 OID 27536)
-- Name: job job_pkey; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.job
    ADD CONSTRAINT job_pkey PRIMARY KEY (id);


--
-- TOC entry 5064 (class 2606 OID 27586)
-- Name: jobparameter jobparameter_pkey; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.jobparameter
    ADD CONSTRAINT jobparameter_pkey PRIMARY KEY (id);


--
-- TOC entry 5050 (class 2606 OID 27609)
-- Name: jobqueue jobqueue_pkey; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.jobqueue
    ADD CONSTRAINT jobqueue_pkey PRIMARY KEY (id);


--
-- TOC entry 5053 (class 2606 OID 27629)
-- Name: list list_pkey; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.list
    ADD CONSTRAINT list_pkey PRIMARY KEY (id);


--
-- TOC entry 5066 (class 2606 OID 27508)
-- Name: lock lock_resource_key; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.lock
    ADD CONSTRAINT lock_resource_key UNIQUE (resource);

ALTER TABLE ONLY hangfire.lock REPLICA IDENTITY USING INDEX lock_resource_key;


--
-- TOC entry 5028 (class 2606 OID 27387)
-- Name: schema schema_pkey; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.schema
    ADD CONSTRAINT schema_pkey PRIMARY KEY (version);


--
-- TOC entry 5055 (class 2606 OID 27655)
-- Name: server server_pkey; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.server
    ADD CONSTRAINT server_pkey PRIMARY KEY (id);


--
-- TOC entry 5059 (class 2606 OID 27657)
-- Name: set set_key_value_key; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.set
    ADD CONSTRAINT set_key_value_key UNIQUE (key, value);


--
-- TOC entry 5061 (class 2606 OID 27638)
-- Name: set set_pkey; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.set
    ADD CONSTRAINT set_pkey PRIMARY KEY (id);


--
-- TOC entry 5045 (class 2606 OID 27563)
-- Name: state state_pkey; Type: CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.state
    ADD CONSTRAINT state_pkey PRIMARY KEY (id);


--
-- TOC entry 5017 (class 2606 OID 26994)
-- Name: ShippingAddresses PK_Addresses; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ShippingAddresses"
    ADD CONSTRAINT "PK_Addresses" PRIMARY KEY ("Id");


--
-- TOC entry 4925 (class 2606 OID 26996)
-- Name: AspNetRoleClaims PK_AspNetRoleClaims; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetRoleClaims"
    ADD CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id");


--
-- TOC entry 4927 (class 2606 OID 26998)
-- Name: AspNetRoles PK_AspNetRoles; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetRoles"
    ADD CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id");


--
-- TOC entry 4931 (class 2606 OID 27000)
-- Name: AspNetUserClaims PK_AspNetUserClaims; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserClaims"
    ADD CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id");


--
-- TOC entry 4934 (class 2606 OID 27002)
-- Name: AspNetUserLogins PK_AspNetUserLogins; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserLogins"
    ADD CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey");


--
-- TOC entry 4938 (class 2606 OID 27004)
-- Name: AspNetUserRoles PK_AspNetUserRoles; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserRoles"
    ADD CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId");


--
-- TOC entry 4940 (class 2606 OID 27006)
-- Name: AspNetUserTokens PK_AspNetUserTokens; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserTokens"
    ADD CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name");


--
-- TOC entry 4944 (class 2606 OID 27008)
-- Name: AspNetUsers PK_AspNetUsers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUsers"
    ADD CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id");


--
-- TOC entry 4949 (class 2606 OID 27010)
-- Name: CartOffers PK_CartOffers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CartOffers"
    ADD CONSTRAINT "PK_CartOffers" PRIMARY KEY ("Id");


--
-- TOC entry 4952 (class 2606 OID 27012)
-- Name: Carts PK_Carts; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Carts"
    ADD CONSTRAINT "PK_Carts" PRIMARY KEY ("Id");


--
-- TOC entry 4954 (class 2606 OID 27014)
-- Name: Categories PK_Categories; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Categories"
    ADD CONSTRAINT "PK_Categories" PRIMARY KEY ("Id");


--
-- TOC entry 4956 (class 2606 OID 27016)
-- Name: Company PK_Company; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Company"
    ADD CONSTRAINT "PK_Company" PRIMARY KEY ("Id");


--
-- TOC entry 4958 (class 2606 OID 27018)
-- Name: Conditions PK_Conditions; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Conditions"
    ADD CONSTRAINT "PK_Conditions" PRIMARY KEY ("Id");


--
-- TOC entry 4960 (class 2606 OID 27020)
-- Name: Countries PK_Countries; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Countries"
    ADD CONSTRAINT "PK_Countries" PRIMARY KEY ("Id");


--
-- TOC entry 4963 (class 2606 OID 27022)
-- Name: Deliveries PK_Deliveries; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Deliveries"
    ADD CONSTRAINT "PK_Deliveries" PRIMARY KEY ("Id");


--
-- TOC entry 4965 (class 2606 OID 27024)
-- Name: DeliveryCarriers PK_DeliveryCarriers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DeliveryCarriers"
    ADD CONSTRAINT "PK_DeliveryCarriers" PRIMARY KEY ("Id");


--
-- TOC entry 4968 (class 2606 OID 27026)
-- Name: Images PK_Images; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Images"
    ADD CONSTRAINT "PK_Images" PRIMARY KEY ("Id");


--
-- TOC entry 4972 (class 2606 OID 27028)
-- Name: Items PK_Items; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Items"
    ADD CONSTRAINT "PK_Items" PRIMARY KEY ("Id");


--
-- TOC entry 4976 (class 2606 OID 27030)
-- Name: OfferDeliveries PK_OfferDeliveries; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OfferDeliveries"
    ADD CONSTRAINT "PK_OfferDeliveries" PRIMARY KEY ("Id");


--
-- TOC entry 4980 (class 2606 OID 27032)
-- Name: Offers PK_Offers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Offers"
    ADD CONSTRAINT "PK_Offers" PRIMARY KEY ("Id");


--
-- TOC entry 4984 (class 2606 OID 27034)
-- Name: OrderDeliveries PK_OrderDeliveries; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OrderDeliveries"
    ADD CONSTRAINT "PK_OrderDeliveries" PRIMARY KEY ("Id");


--
-- TOC entry 4987 (class 2606 OID 27038)
-- Name: OrderLines PK_OrderLines; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OrderLines"
    ADD CONSTRAINT "PK_OrderLines" PRIMARY KEY ("Id");


--
-- TOC entry 4990 (class 2606 OID 27040)
-- Name: Orders PK_Orders; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "PK_Orders" PRIMARY KEY ("Id");


--
-- TOC entry 5026 (class 2606 OID 27330)
-- Name: PaymentDetails PK_PaymentDetails; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PaymentDetails"
    ADD CONSTRAINT "PK_PaymentDetails" PRIMARY KEY ("Id");


--
-- TOC entry 4994 (class 2606 OID 27042)
-- Name: PaymentOrders PK_PaymentOrders; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PaymentOrders"
    ADD CONSTRAINT "PK_PaymentOrders" PRIMARY KEY ("Id");


--
-- TOC entry 4996 (class 2606 OID 27044)
-- Name: Payments PK_Payments; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Payments"
    ADD CONSTRAINT "PK_Payments" PRIMARY KEY ("Id");


--
-- TOC entry 4998 (class 2606 OID 27046)
-- Name: Permissions PK_Permissions; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Permissions"
    ADD CONSTRAINT "PK_Permissions" PRIMARY KEY ("Id");


--
-- TOC entry 5000 (class 2606 OID 27048)
-- Name: RentCartOffers PK_RentCartOffers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."RentCartOffers"
    ADD CONSTRAINT "PK_RentCartOffers" PRIMARY KEY ("Id");


--
-- TOC entry 5002 (class 2606 OID 27050)
-- Name: RentOffers PK_RentOffers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."RentOffers"
    ADD CONSTRAINT "PK_RentOffers" PRIMARY KEY ("Id");


--
-- TOC entry 5005 (class 2606 OID 27376)
-- Name: Rentals PK_Rentals; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Rentals"
    ADD CONSTRAINT "PK_Rentals" PRIMARY KEY ("Id");


--
-- TOC entry 5009 (class 2606 OID 27054)
-- Name: RolePermissions PK_RolePermissions; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."RolePermissions"
    ADD CONSTRAINT "PK_RolePermissions" PRIMARY KEY ("Id");


--
-- TOC entry 5011 (class 2606 OID 27056)
-- Name: SaleCartOffers PK_SaleCartOffers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."SaleCartOffers"
    ADD CONSTRAINT "PK_SaleCartOffers" PRIMARY KEY ("Id");


--
-- TOC entry 5013 (class 2606 OID 27058)
-- Name: SaleOffers PK_SaleOffers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."SaleOffers"
    ADD CONSTRAINT "PK_SaleOffers" PRIMARY KEY ("Id");


--
-- TOC entry 5021 (class 2606 OID 27060)
-- Name: UserPermissions PK_UserPermissions; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."UserPermissions"
    ADD CONSTRAINT "PK_UserPermissions" PRIMARY KEY ("Id");


--
-- TOC entry 5023 (class 2606 OID 27062)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 5031 (class 1259 OID 27691)
-- Name: ix_hangfire_counter_expireat; Type: INDEX; Schema: hangfire; Owner: postgres
--

CREATE INDEX ix_hangfire_counter_expireat ON hangfire.counter USING btree (expireat);


--
-- TOC entry 5032 (class 1259 OID 27646)
-- Name: ix_hangfire_counter_key; Type: INDEX; Schema: hangfire; Owner: postgres
--

CREATE INDEX ix_hangfire_counter_key ON hangfire.counter USING btree (key);


--
-- TOC entry 5037 (class 1259 OID 27700)
-- Name: ix_hangfire_hash_expireat; Type: INDEX; Schema: hangfire; Owner: postgres
--

CREATE INDEX ix_hangfire_hash_expireat ON hangfire.hash USING btree (expireat);


--
-- TOC entry 5038 (class 1259 OID 27717)
-- Name: ix_hangfire_job_expireat; Type: INDEX; Schema: hangfire; Owner: postgres
--

CREATE INDEX ix_hangfire_job_expireat ON hangfire.job USING btree (expireat);


--
-- TOC entry 5039 (class 1259 OID 27653)
-- Name: ix_hangfire_job_statename; Type: INDEX; Schema: hangfire; Owner: postgres
--

CREATE INDEX ix_hangfire_job_statename ON hangfire.job USING btree (statename);


--
-- TOC entry 5040 (class 1259 OID 27804)
-- Name: ix_hangfire_job_statename_is_not_null; Type: INDEX; Schema: hangfire; Owner: postgres
--

CREATE INDEX ix_hangfire_job_statename_is_not_null ON hangfire.job USING btree (statename) INCLUDE (id) WHERE (statename IS NOT NULL);


--
-- TOC entry 5062 (class 1259 OID 27658)
-- Name: ix_hangfire_jobparameter_jobidandname; Type: INDEX; Schema: hangfire; Owner: postgres
--

CREATE INDEX ix_hangfire_jobparameter_jobidandname ON hangfire.jobparameter USING btree (jobid, name);


--
-- TOC entry 5046 (class 1259 OID 27803)
-- Name: ix_hangfire_jobqueue_fetchedat_queue_jobid; Type: INDEX; Schema: hangfire; Owner: postgres
--

CREATE INDEX ix_hangfire_jobqueue_fetchedat_queue_jobid ON hangfire.jobqueue USING btree (fetchedat NULLS FIRST, queue, jobid);


--
-- TOC entry 5047 (class 1259 OID 27618)
-- Name: ix_hangfire_jobqueue_jobidandqueue; Type: INDEX; Schema: hangfire; Owner: postgres
--

CREATE INDEX ix_hangfire_jobqueue_jobidandqueue ON hangfire.jobqueue USING btree (jobid, queue);


--
-- TOC entry 5048 (class 1259 OID 27726)
-- Name: ix_hangfire_jobqueue_queueandfetchedat; Type: INDEX; Schema: hangfire; Owner: postgres
--

CREATE INDEX ix_hangfire_jobqueue_queueandfetchedat ON hangfire.jobqueue USING btree (queue, fetchedat);


--
-- TOC entry 5051 (class 1259 OID 27737)
-- Name: ix_hangfire_list_expireat; Type: INDEX; Schema: hangfire; Owner: postgres
--

CREATE INDEX ix_hangfire_list_expireat ON hangfire.list USING btree (expireat);


--
-- TOC entry 5056 (class 1259 OID 27757)
-- Name: ix_hangfire_set_expireat; Type: INDEX; Schema: hangfire; Owner: postgres
--

CREATE INDEX ix_hangfire_set_expireat ON hangfire.set USING btree (expireat);


--
-- TOC entry 5057 (class 1259 OID 27672)
-- Name: ix_hangfire_set_key_score; Type: INDEX; Schema: hangfire; Owner: postgres
--

CREATE INDEX ix_hangfire_set_key_score ON hangfire.set USING btree (key, score);


--
-- TOC entry 5043 (class 1259 OID 27571)
-- Name: ix_hangfire_state_jobid; Type: INDEX; Schema: hangfire; Owner: postgres
--

CREATE INDEX ix_hangfire_state_jobid ON hangfire.state USING btree (jobid);


--
-- TOC entry 4941 (class 1259 OID 27063)
-- Name: EmailIndex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "EmailIndex" ON public."AspNetUsers" USING btree ("NormalizedEmail");


--
-- TOC entry 5014 (class 1259 OID 27064)
-- Name: IX_Addresses_CountryId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Addresses_CountryId" ON public."ShippingAddresses" USING btree ("CountryId");


--
-- TOC entry 5015 (class 1259 OID 27065)
-- Name: IX_Addresses_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Addresses_UserId" ON public."ShippingAddresses" USING btree ("UserId");


--
-- TOC entry 4923 (class 1259 OID 27066)
-- Name: IX_AspNetRoleClaims_RoleId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON public."AspNetRoleClaims" USING btree ("RoleId");


--
-- TOC entry 4929 (class 1259 OID 27067)
-- Name: IX_AspNetUserClaims_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AspNetUserClaims_UserId" ON public."AspNetUserClaims" USING btree ("UserId");


--
-- TOC entry 4932 (class 1259 OID 27068)
-- Name: IX_AspNetUserLogins_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AspNetUserLogins_UserId" ON public."AspNetUserLogins" USING btree ("UserId");


--
-- TOC entry 4935 (class 1259 OID 27069)
-- Name: IX_AspNetUserRoles_RoleId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON public."AspNetUserRoles" USING btree ("RoleId");


--
-- TOC entry 4936 (class 1259 OID 27070)
-- Name: IX_AspNetUserRoles_UserId_RoleId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_AspNetUserRoles_UserId_RoleId" ON public."AspNetUserRoles" USING btree ("UserId", "RoleId");


--
-- TOC entry 4942 (class 1259 OID 27071)
-- Name: IX_AspNetUsers_CompanyId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AspNetUsers_CompanyId" ON public."AspNetUsers" USING btree ("CompanyId");


--
-- TOC entry 4946 (class 1259 OID 27072)
-- Name: IX_CartOffers_CartId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_CartOffers_CartId" ON public."CartOffers" USING btree ("CartId");


--
-- TOC entry 4947 (class 1259 OID 27073)
-- Name: IX_CartOffers_OfferId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_CartOffers_OfferId" ON public."CartOffers" USING btree ("OfferId");


--
-- TOC entry 4950 (class 1259 OID 27074)
-- Name: IX_Carts_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_Carts_UserId" ON public."Carts" USING btree ("UserId");


--
-- TOC entry 4961 (class 1259 OID 27075)
-- Name: IX_Deliveries_DeliveryCarrierId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Deliveries_DeliveryCarrierId" ON public."Deliveries" USING btree ("DeliveryCarrierId");


--
-- TOC entry 4966 (class 1259 OID 27076)
-- Name: IX_Images_ItemId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Images_ItemId" ON public."Images" USING btree ("ItemId");


--
-- TOC entry 4969 (class 1259 OID 27077)
-- Name: IX_Items_CategoryId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Items_CategoryId" ON public."Items" USING btree ("CategoryId");


--
-- TOC entry 4970 (class 1259 OID 27078)
-- Name: IX_Items_ConditionId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Items_ConditionId" ON public."Items" USING btree ("ConditionId");


--
-- TOC entry 4973 (class 1259 OID 27079)
-- Name: IX_OfferDeliveries_DeliveryId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_OfferDeliveries_DeliveryId" ON public."OfferDeliveries" USING btree ("DeliveryId");


--
-- TOC entry 4974 (class 1259 OID 27080)
-- Name: IX_OfferDeliveries_OfferId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_OfferDeliveries_OfferId" ON public."OfferDeliveries" USING btree ("OfferId");


--
-- TOC entry 4977 (class 1259 OID 27081)
-- Name: IX_Offers_CreatedByUserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Offers_CreatedByUserId" ON public."Offers" USING btree ("CreatedByUserId");


--
-- TOC entry 4978 (class 1259 OID 27082)
-- Name: IX_Offers_ItemId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Offers_ItemId" ON public."Offers" USING btree ("ItemId");


--
-- TOC entry 4981 (class 1259 OID 27083)
-- Name: IX_OrderDeliveries_Channel; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_OrderDeliveries_Channel" ON public."OrderDeliveries" USING btree ("Channel");


--
-- TOC entry 4982 (class 1259 OID 27084)
-- Name: IX_OrderDeliveries_OrderId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_OrderDeliveries_OrderId" ON public."OrderDeliveries" USING btree ("OrderId");


--
-- TOC entry 4985 (class 1259 OID 27086)
-- Name: IX_OrderLines_OrderId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_OrderLines_OrderId" ON public."OrderLines" USING btree ("OrderId");


--
-- TOC entry 4988 (class 1259 OID 27087)
-- Name: IX_Orders_BuyerId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Orders_BuyerId" ON public."Orders" USING btree ("BuyerId");


--
-- TOC entry 5024 (class 1259 OID 27336)
-- Name: IX_PaymentDetails_PaymentId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_PaymentDetails_PaymentId" ON public."PaymentDetails" USING btree ("PaymentId");


--
-- TOC entry 4991 (class 1259 OID 27317)
-- Name: IX_PaymentOrders_OrderId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_PaymentOrders_OrderId" ON public."PaymentOrders" USING btree ("OrderId");


--
-- TOC entry 4992 (class 1259 OID 27091)
-- Name: IX_PaymentOrders_PaymentId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_PaymentOrders_PaymentId" ON public."PaymentOrders" USING btree ("PaymentId");


--
-- TOC entry 5003 (class 1259 OID 27092)
-- Name: IX_Rentals_BorrowerId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Rentals_BorrowerId" ON public."Rentals" USING btree ("BorrowerId");


--
-- TOC entry 5006 (class 1259 OID 27093)
-- Name: IX_RolePermissions_PermissionId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_RolePermissions_PermissionId" ON public."RolePermissions" USING btree ("PermissionId");


--
-- TOC entry 5007 (class 1259 OID 27094)
-- Name: IX_RolePermissions_RoleId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_RolePermissions_RoleId" ON public."RolePermissions" USING btree ("RoleId");


--
-- TOC entry 5018 (class 1259 OID 27095)
-- Name: IX_UserPermissions_PermissionId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_UserPermissions_PermissionId" ON public."UserPermissions" USING btree ("PermissionId");


--
-- TOC entry 5019 (class 1259 OID 27096)
-- Name: IX_UserPermissions_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_UserPermissions_UserId" ON public."UserPermissions" USING btree ("UserId");


--
-- TOC entry 4928 (class 1259 OID 27097)
-- Name: RoleNameIndex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "RoleNameIndex" ON public."AspNetRoles" USING btree ("NormalizedName");


--
-- TOC entry 4945 (class 1259 OID 27098)
-- Name: UserNameIndex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "UserNameIndex" ON public."AspNetUsers" USING btree ("NormalizedUserName");


--
-- TOC entry 5107 (class 2606 OID 27595)
-- Name: jobparameter jobparameter_jobid_fkey; Type: FK CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.jobparameter
    ADD CONSTRAINT jobparameter_jobid_fkey FOREIGN KEY (jobid) REFERENCES hangfire.job(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 5106 (class 2606 OID 27572)
-- Name: state state_jobid_fkey; Type: FK CONSTRAINT; Schema: hangfire; Owner: postgres
--

ALTER TABLE ONLY hangfire.state
    ADD CONSTRAINT state_jobid_fkey FOREIGN KEY (jobid) REFERENCES hangfire.job(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 5101 (class 2606 OID 27099)
-- Name: ShippingAddresses FK_Addresses_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ShippingAddresses"
    ADD CONSTRAINT "FK_Addresses_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id");


--
-- TOC entry 5102 (class 2606 OID 27104)
-- Name: ShippingAddresses FK_Addresses_Countries_CountryId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ShippingAddresses"
    ADD CONSTRAINT "FK_Addresses_Countries_CountryId" FOREIGN KEY ("CountryId") REFERENCES public."Countries"("Id");


--
-- TOC entry 5071 (class 2606 OID 27109)
-- Name: AspNetRoleClaims FK_AspNetRoleClaims_AspNetRoles_RoleId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetRoleClaims"
    ADD CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES public."AspNetRoles"("Id") ON DELETE CASCADE;


--
-- TOC entry 5072 (class 2606 OID 27114)
-- Name: AspNetUserClaims FK_AspNetUserClaims_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserClaims"
    ADD CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 5073 (class 2606 OID 27119)
-- Name: AspNetUserLogins FK_AspNetUserLogins_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserLogins"
    ADD CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 5074 (class 2606 OID 27124)
-- Name: AspNetUserRoles FK_AspNetUserRoles_AspNetRoles_RoleId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserRoles"
    ADD CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES public."AspNetRoles"("Id");


--
-- TOC entry 5075 (class 2606 OID 27129)
-- Name: AspNetUserRoles FK_AspNetUserRoles_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserRoles"
    ADD CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id");


--
-- TOC entry 5076 (class 2606 OID 27134)
-- Name: AspNetUserTokens FK_AspNetUserTokens_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUserTokens"
    ADD CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 5077 (class 2606 OID 27139)
-- Name: AspNetUsers FK_AspNetUsers_Company_CompanyId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AspNetUsers"
    ADD CONSTRAINT "FK_AspNetUsers_Company_CompanyId" FOREIGN KEY ("CompanyId") REFERENCES public."Company"("Id");


--
-- TOC entry 5078 (class 2606 OID 27144)
-- Name: CartOffers FK_CartOffers_Carts_CartId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CartOffers"
    ADD CONSTRAINT "FK_CartOffers_Carts_CartId" FOREIGN KEY ("CartId") REFERENCES public."Carts"("Id");


--
-- TOC entry 5079 (class 2606 OID 27149)
-- Name: CartOffers FK_CartOffers_Offers_OfferId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CartOffers"
    ADD CONSTRAINT "FK_CartOffers_Offers_OfferId" FOREIGN KEY ("OfferId") REFERENCES public."Offers"("Id");


--
-- TOC entry 5080 (class 2606 OID 27154)
-- Name: Carts FK_Carts_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Carts"
    ADD CONSTRAINT "FK_Carts_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id");


--
-- TOC entry 5081 (class 2606 OID 27159)
-- Name: Deliveries FK_Deliveries_DeliveryCarriers_DeliveryCarrierId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Deliveries"
    ADD CONSTRAINT "FK_Deliveries_DeliveryCarriers_DeliveryCarrierId" FOREIGN KEY ("DeliveryCarrierId") REFERENCES public."DeliveryCarriers"("Id");


--
-- TOC entry 5082 (class 2606 OID 27164)
-- Name: Images FK_Images_Items_ItemId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Images"
    ADD CONSTRAINT "FK_Images_Items_ItemId" FOREIGN KEY ("ItemId") REFERENCES public."Items"("Id");


--
-- TOC entry 5083 (class 2606 OID 27169)
-- Name: Items FK_Items_Categories_CategoryId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Items"
    ADD CONSTRAINT "FK_Items_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES public."Categories"("Id");


--
-- TOC entry 5084 (class 2606 OID 27174)
-- Name: Items FK_Items_Conditions_ConditionId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Items"
    ADD CONSTRAINT "FK_Items_Conditions_ConditionId" FOREIGN KEY ("ConditionId") REFERENCES public."Conditions"("Id");


--
-- TOC entry 5085 (class 2606 OID 27179)
-- Name: OfferDeliveries FK_OfferDeliveries_Deliveries_DeliveryId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OfferDeliveries"
    ADD CONSTRAINT "FK_OfferDeliveries_Deliveries_DeliveryId" FOREIGN KEY ("DeliveryId") REFERENCES public."Deliveries"("Id");


--
-- TOC entry 5086 (class 2606 OID 27184)
-- Name: OfferDeliveries FK_OfferDeliveries_Offers_OfferId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OfferDeliveries"
    ADD CONSTRAINT "FK_OfferDeliveries_Offers_OfferId" FOREIGN KEY ("OfferId") REFERENCES public."Offers"("Id");


--
-- TOC entry 5087 (class 2606 OID 27189)
-- Name: Offers FK_Offers_AspNetUsers_CreatedByUserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Offers"
    ADD CONSTRAINT "FK_Offers_AspNetUsers_CreatedByUserId" FOREIGN KEY ("CreatedByUserId") REFERENCES public."AspNetUsers"("Id");


--
-- TOC entry 5088 (class 2606 OID 27194)
-- Name: Offers FK_Offers_Items_ItemId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Offers"
    ADD CONSTRAINT "FK_Offers_Items_ItemId" FOREIGN KEY ("ItemId") REFERENCES public."Items"("Id");


--
-- TOC entry 5089 (class 2606 OID 27199)
-- Name: OrderDeliveries FK_OrderDeliveries_Orders_OrderId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OrderDeliveries"
    ADD CONSTRAINT "FK_OrderDeliveries_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES public."Orders"("Id") ON DELETE CASCADE;


--
-- TOC entry 5090 (class 2606 OID 27209)
-- Name: OrderLines FK_OrderLines_Orders_OrderId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OrderLines"
    ADD CONSTRAINT "FK_OrderLines_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES public."Orders"("Id");


--
-- TOC entry 5091 (class 2606 OID 27214)
-- Name: Orders FK_Orders_AspNetUsers_BuyerId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "FK_Orders_AspNetUsers_BuyerId" FOREIGN KEY ("BuyerId") REFERENCES public."AspNetUsers"("Id");


--
-- TOC entry 5105 (class 2606 OID 27331)
-- Name: PaymentDetails FK_PaymentDetails_Payments_PaymentId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PaymentDetails"
    ADD CONSTRAINT "FK_PaymentDetails_Payments_PaymentId" FOREIGN KEY ("PaymentId") REFERENCES public."Payments"("Id");


--
-- TOC entry 5092 (class 2606 OID 27318)
-- Name: PaymentOrders FK_PaymentOrders_Orders_OrderId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PaymentOrders"
    ADD CONSTRAINT "FK_PaymentOrders_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES public."Orders"("Id");


--
-- TOC entry 5093 (class 2606 OID 27234)
-- Name: PaymentOrders FK_PaymentOrders_Payments_PaymentId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PaymentOrders"
    ADD CONSTRAINT "FK_PaymentOrders_Payments_PaymentId" FOREIGN KEY ("PaymentId") REFERENCES public."Payments"("Id");


--
-- TOC entry 5094 (class 2606 OID 27239)
-- Name: RentCartOffers FK_RentCartOffers_CartOffers_Id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."RentCartOffers"
    ADD CONSTRAINT "FK_RentCartOffers_CartOffers_Id" FOREIGN KEY ("Id") REFERENCES public."CartOffers"("Id") ON DELETE CASCADE;


--
-- TOC entry 5095 (class 2606 OID 27244)
-- Name: RentOffers FK_RentOffers_Offers_Id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."RentOffers"
    ADD CONSTRAINT "FK_RentOffers_Offers_Id" FOREIGN KEY ("Id") REFERENCES public."Offers"("Id") ON DELETE CASCADE;


--
-- TOC entry 5096 (class 2606 OID 27377)
-- Name: Rentals FK_Rentals_AspNetUsers_BorrowerId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Rentals"
    ADD CONSTRAINT "FK_Rentals_AspNetUsers_BorrowerId" FOREIGN KEY ("BorrowerId") REFERENCES public."AspNetUsers"("Id");


--
-- TOC entry 5097 (class 2606 OID 27254)
-- Name: RolePermissions FK_RolePermissions_AspNetRoles_RoleId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."RolePermissions"
    ADD CONSTRAINT "FK_RolePermissions_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES public."AspNetRoles"("Id");


--
-- TOC entry 5098 (class 2606 OID 27259)
-- Name: RolePermissions FK_RolePermissions_Permissions_PermissionId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."RolePermissions"
    ADD CONSTRAINT "FK_RolePermissions_Permissions_PermissionId" FOREIGN KEY ("PermissionId") REFERENCES public."Permissions"("Id");


--
-- TOC entry 5099 (class 2606 OID 27264)
-- Name: SaleCartOffers FK_SaleCartOffers_CartOffers_Id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."SaleCartOffers"
    ADD CONSTRAINT "FK_SaleCartOffers_CartOffers_Id" FOREIGN KEY ("Id") REFERENCES public."CartOffers"("Id") ON DELETE CASCADE;


--
-- TOC entry 5100 (class 2606 OID 27269)
-- Name: SaleOffers FK_SaleOffers_Offers_Id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."SaleOffers"
    ADD CONSTRAINT "FK_SaleOffers_Offers_Id" FOREIGN KEY ("Id") REFERENCES public."Offers"("Id") ON DELETE CASCADE;


--
-- TOC entry 5103 (class 2606 OID 27274)
-- Name: UserPermissions FK_UserPermissions_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."UserPermissions"
    ADD CONSTRAINT "FK_UserPermissions_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES public."AspNetUsers"("Id");


--
-- TOC entry 5104 (class 2606 OID 27279)
-- Name: UserPermissions FK_UserPermissions_Permissions_PermissionId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."UserPermissions"
    ADD CONSTRAINT "FK_UserPermissions_Permissions_PermissionId" FOREIGN KEY ("PermissionId") REFERENCES public."Permissions"("Id");


-- Completed on 2026-02-21 17:49:18

--
-- PostgreSQL database dump complete
--

\unrestrict 5hVgM3Od0QperTuyYaXf119DnS8i4tBnPZkmFPN83T9WQvpgyYiZWEFn8xdSna0

