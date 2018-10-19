use master
go
if DB_ID(N'VehicleManagementSystem') is null
create database VehicleManagementSystem
on
(
    name='VehicleManagementSystem',
	filename='D:\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\VehicleManagementSystem_data.mdf',
	size=10,
	maxsize=50,
	filegrowth=5
)
log on
(
    name='VehicleManagementSystem_log',
	filename='D:\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\VehicleManagementSystem_log.ldf',
	size=10,
	maxsize=50,
	filegrowth=5
)
go
use VehicleManagementSystem
go
--驾驶证信息表
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_normal_driver_license'))
create table t_normal_driver_license
(
  id decimal(16,0) identity(1,1) primary key,
  id_no varchar(50) not null,
  name varchar(20) not null,
  sex char(1) not null,
  birthday datetime null,
  addr varchar(200) not null,
  region_no varchar(4) not null,
  permitted_card_type_no varchar(4) not null,
  work_unit varchar(100) null,
  first_get_license_date datetime not null,
  valid_date_start datetime not null,
  valid_date_end datetime not null,
  status char(1) not null default('0'),
  oper_date datetime not null default(getdate()),
  oper_id varchar(10) not null,
  modify_date datetime null,
  modiry_oper_id varchar(10) null,
  time_stamp timestamp,
  img_url varchar(200) null,
  img0_url varchar(200) null,
  img1_url varchar(200) null,
  img2_url varchar(200) null,
  img3_url varchar(200) null,
  img4_url varchar(200) null,
  img5_url varchar(200) null
)
go
--行驶证信息
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_normal_car_license'))
create table t_normal_car_license
(
  id decimal(16,0) identity(1,1) primary key,
  id_no varchar(50) not null,
  car_owner varchar(20) not null,
  duty varchar(50) null,
  work_unit varchar(100) null,
  region_no varchar(4) not null,
  addr varchar(200) not null,
  plate_no varchar(50) not null,
  car_type varchar(100) not null,
  brand_no varchar(50) not null,
  motor_no varchar(100) not null,
  carframe_no varchar(100) not null,
  car_color varchar(50) not null,
  product_date datetime not null,
  issue_license_date datetime not null,
  status char(1) not null default('0'),
  oper_date datetime not null default(getdate()),
  oper_id varchar(10) not null,
  modify_date datetime null,
  modify_oper_id varchar(10) null,
  time_stamp timestamp,
  img_url varchar(200) null,
  img0_url varchar(200) null,
  img1_url varchar(200) null,
  img2_url varchar(200) null,
  img3_url varchar(200) null,
  img4_url varchar(200) null,
  img5_url varchar(200) null
)
go
--临时驾驶证信息
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_temp_driver_license'))
create table t_temp_driver_license
(
  id decimal(16,0) identity(1,1) primary key,
  name varchar(20) not null,
  sex char(1) not null,
  birthday datetime null,
  folk varchar(50) not null,
  now_addr varchar(200) not null,
  old_addr varchar(200) not null,
  region_no varchar(4) not null,
  permitted_card_type_no varchar(4) not null,
  check_man varchar(20) not null,
  check_date datetime not null,
  nation_no varchar(10) not null,
  status char(1) not null default('0'),
  oper_date datetime not null,
  oper_id varchar(10) not null,
  modify_date datetime null,
  modify_oper_id varchar(10) null,
  time_stamp timestamp,
  img_url varchar(200) null,
  img0_url varchar(200) null,
  img1_url varchar(200) null,
  img2_url varchar(200) null,
  img3_url varchar(200) null,
  img4_url varchar(200) null,
  img5_url varchar(200) null
)
go
--准驾车型
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_bd_permitted_car_type'))
create table t_bd_permitted_car_type
(
   type_no varchar(4) not null primary key,
   type_name varchar(50) not null,
   memo varchar(100) null
)
go
--临时行驶证
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_temp_car_license'))
create table t_temp_car_license
(
  id decimal(16,0) identity(1,1) primary key,
  name varchar(20) not null,
  sex char(1) not null,
  birthday datetime null,
  folk varchar(50) not null,
  now_addr varchar(200) not null,
  old_addr varchar(200) not null,
  region_no varchar(4) not null,
  permitted_car_type_no varchar(4) not null,
  check_man varchar(20) not null,
  check_date datetime not null,
  nation_no varchar(4) not null,
  status char(1) not null default('0'),
  oper_date datetime not null,
  oper_id varchar(10) not null,
  modify_date datetime null,
  modify_oper_id varchar(10) null,
  time_stamp timestamp,
  img_url varchar(200) null,
  img0_url varchar(200) null,
  img1_url varchar(200) null,
  img2_url varchar(200) null,
  img3_url varchar(200) null,
  img4_url varchar(200) null,
  img5_url varchar(200) null
)
go
--区域信息
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_bd_region'))
create table t_bd_region
(
   region_no varchar(4) not null primary key,
   region_name varchar(50) not null,
   memo varchar(100) null
)
go
--事故登记
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_accident_records'))
create table t_accident_records
(
  id decimal(16,0) identity(1,1) primary key,
  happen_date datetime not null,
  happen_addr varchar(200) null,
  first_party_man varchar(50) not null,
  first_party_addr varchar(200) not null,
  second_party_man varchar(50) not null,
  second_party_addr varchar(200) not null,
  accident_desc varchar(500) not null,
  mediation_unit varchar(200) not null,
  mediation_date datetime not null,
  draw_recorder varchar(50) not null,
  accident_mediator varchar(50) not null,
  oper_id varchar(10) not null,
  oper_date datetime not null default(getdate()),
  modify_date datetime null,
  modify_oper_id varchar(10) null,
  time_stamp timestamp,
  img_url varchar(200) null,
  img0_url varchar(200) null,
  img1_url varchar(200) null,
)
go
--消防设备登记
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_fire_equipment_register'))
create table t_fire_equipment_register
(
   id decimal(16,0) identity(1,1) primary key,
   eq_name varchar(200) not null,
   install_addr varchar(200) not null,
   usage_desc varchar(200) not null,
   install_date datetime not null,
   person_liable varchar(50) not null,
   oper_id varchar(10) not null,
   oper_date datetime not null,
   modify_oper_id varchar(10) null,
   modify_date datetime null,
   time_stamp timestamp,
   img_url varchar(200) null,
   img0_url varchar(200) null,
   img1_url varchar(200) null,
)
go
--消防事故登记
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_fire_accident_records'))
create table t_fire_accident_records
(
  id decimal(16,0) identity(1,1) primary key,
  happen_date datetime not null,
  happen_addr varchar(200) null,
  accident_desc varchar(500) not null,
  out_police_cars varchar(200) not null,
  out_police_mans varchar(200) not null,
  process_results varchar(200) not null,
  oper_id varchar(10) not null,
  oper_date datetime not null,
  modify_oper_id varchar(10) null,
  modify_date datetime null,
  time_stamp timestamp,
  img_url varchar(200) null,
  img0_url varchar(200) null,
  img1_url varchar(200) null,
)
go
--系统操作日志
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_operate_log'))
create table t_sys_operate_log
(
  id decimal(16,0) identity(1,1) primary key,
  region_no varchar(4) null,
  oper_desc varchar(100) null,
  memo varchar(500) null,
  oper_id varchar(10) not null,
  oper_date datetime not null default(getdate())
)
go
--登陆日志
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_login_log'))
create table t_sys_login_log
(
  id decimal(16,0) identity(1,1) primary key,
  region_no varchar(4) null,
  ip varchar(100) not null,
  login_id varchar(10) not null,
  login_date datetime not null
)
go
--打印日志
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_print_log'))
create table t_sys_print_log
(
  id decimal(16,0) identity(1,1) primary key,
  ip varchar(100) not null,
  print_type varchar(100) not null,
  printer_id varchar(10) not null,
  print_date datetime not null
)
go
--用户表
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_user'))
create table t_sys_user
(
  id decimal(16,0) identity(1,1) primary key,
  user_id varchar(10) not null,
  user_pwd varchar(100) not null,
  user_name varchar(100) not null,
  sex char(1) not null,
  age int not null,
  tel varchar(20) not null,
  email varchar(50) null,
  status char(1) not null default('0'),
  last_login_time datetime null,
  create_date datetime not null default(getdate())
)
go
--组
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_group'))
create table t_sys_group
(
  id decimal(16,0) identity(1,1) primary key,
  group_name varchar(50) not null,
  create_id varchar(10) not null,
  create_date datetime not null default(getdate()),
  status char(1) not null default('0'),
  memo varchar(100) not null
)
go
--用户组
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_user_group'))
create table t_sys_user_group
(
  id decimal(16,0) identity(1,1) primary key,
  user_id varchar(10)  null,
  group_id decimal(16,0) null,
  create_id varchar(10) not null,
  create_date datetime not null default(getdate())
)
go
--组角色
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_group_role'))
create table t_sys_group_role
(
  id decimal(16,0) identity(1,1) primary key,
  role_id decimal(16,0)  null,
  group_id decimal(16,0) null,
  create_id varchar(10) not null,
  create_date datetime not null default(getdate())
)
go
--角色
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_role'))
create table t_sys_role
(
  id decimal(16,0) identity(1,1) primary key,
  role_name varchar(100) not null,
  create_id varchar(10) not null,
  create_date datetime not null default(getdate()),
  status char(1) not null default('0'),
  memo varchar(100) null
)
go
--用户角色
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_oper_role'))
create table t_sys_oper_role
(
  id decimal(16,0) identity(1,1) primary key,
  role_id varchar(100) not null,
  user_id varchar(10) not null
)
go
--资源
--drop table t_sys_resource
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_resource'))
create table t_sys_resource
(
  id varchar(4) primary key,
  pid varchar(4) null,
  level varchar(10) not null,
  res_uri varchar(200) null,
  res_img varchar(100) null,
  res_desc varchar(100) null,
  res_type_id varchar(50) not null,
  res_type_oper_id varchar(50) not null,
  create_id varchar(10) null,
  create_date datetime not null default(getdate()),
  sort_code int not null
)
go
--资源类型
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_resource_type'))
create table t_sys_resource_type
(
  id varchar(4) primary key,
  type_name varchar(20) not null,
  type_desc varchar(200)  null
)
--资源类型操作
--drop table t_sys_res_type_operation
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_res_type_operation'))
create table t_sys_res_type_operation
(
  id varchar(4) primary key,
  type_id varchar(4) not null,
  oper_code varchar(50) not null,
  oper_desc varchar(100)  null
)
go
--角色权限
if not exists(select * from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_role_right'))
create table t_sys_role_right
(
  id decimal(16,0) identity(1,1) primary key,
  role_id decimal(16,0) not null,
  res_id varchar(4) not null,
  grant_id varchar(4) not null,
)
go