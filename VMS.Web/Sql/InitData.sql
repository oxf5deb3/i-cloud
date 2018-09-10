--准驾车型
if exists(select 1 from sysobjects where xtype='U' and id=OBJECT_ID('t_bd_permitted_car_type'))
begin
    truncate table t_bd_permitted_car_type
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WA','大型客车和B','系统默认')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WB','大型货车和C.M','系统默认')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WC','小型汽车','系统默认')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WD','方向把式三轮摩托车和E.L','系统默认')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WE','二轮摩托车和F','系统默认')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WF','轻便摩托车','系统默认')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WG','大型拖拉机,四轮','系统默认')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WH','小型拖拉机','系统默认')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WK','手扶拖拉机','系统默认')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WL','三轮农用运输车','系统默认')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WM','轮式自行专用机械','系统默认')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WN','无轨电车','系统默认')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WQ','电瓶车','系统默认')
end
go
if exists(select 1 from sysobjects where xtype='U' and id=OBJECT_ID('t_bd_region'))
begin
 truncate table t_bd_region
 insert into t_bd_region(region_no,region_name,memo) values('0001','佤邦','系统默认')
end
go





--资源类型
if exists(select 1 from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_resource_type'))
begin
  truncate table t_sys_resource_type
  insert into t_sys_resource_type(id,type_name,type_desc) values('0000','Menu','菜单')
  insert into t_sys_resource_type(id,type_name,type_desc) values('0001','Button','按钮')
end
go

--资源操作
if exists(select 1 from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_res_type_operation'))
begin
   truncate table t_sys_res_type_operation
   insert into t_sys_res_type_operation(id,type_id,oper_code,oper_desc) values('0000','0000','Open','可见')
   insert into t_sys_res_type_operation(id,type_id,oper_code,oper_desc) values('0001','0001','Add','添加')
   insert into t_sys_res_type_operation(id,type_id,oper_code,oper_desc) values('0002','0001','Edit','编辑')
   insert into t_sys_res_type_operation(id,type_id,oper_code,oper_desc) values('0003','0001','Delete','删除')
   insert into t_sys_res_type_operation(id,type_id,oper_code,oper_desc) values('0004','0001','Export','导出')
   insert into t_sys_res_type_operation(id,type_id,oper_code,oper_desc) values('0005','0001','Print','打印')
end
go

--资源
if exists(select 1 from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_resource'))
begin
   truncate table t_sys_resource
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('0000','','0','','glyphicon glyphicon-home','基础资料','0000','1001',GETDATE(),0)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('0001','0000','1','../BaseData/PermittedCarType/Index','','准驾车型','0000','1001',GETDATE(),10)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('0002','0000','1','../BaseData/Region/Index','','区域信息','0000','1001',GETDATE(),11)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('1000','','0','','glyphicon glyphicon-book','登记中心','0000','1001',GETDATE(),1)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('1001','1000','1','../RegisterCenter/TempDriverLicenseRegister/Index','','临时驾驶证登记','0000','1001',GETDATE(),12)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('1002','1000','1','../RegisterCenter/DriverLicenseRegister/Index','','驾驶证登记','0000','1001',GETDATE(),13)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('1003','1000','1','../RegisterCenter/TempDrivingLicenseRegister/Index','','临时行驶证登记','0000','1001',GETDATE(),14)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('1004','1000','1','../RegisterCenter/DrivingLicenseRegister/Index','','行驶证登记','0000','1001',GETDATE(),15)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('2000','','0','','glyphicon glyphicon-book','驾驶证管理','0000','1001',GETDATE(),2)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('2001','2000','1','../DriverLicenseManagement/DriverLicense/Index','','驾驶证查询','0000','1001',GETDATE(),16)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('2002','2000','1','../DriverLicenseManagement/TempDriverLicense/Index','','临时驾驶证查询','0000','1001',GETDATE(),17)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('3000','','0','','glyphicon glyphicon-book','行驶证管理','0000','1001',GETDATE(),3)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('3001','3000','1','../DrivingLicenseManagement/DrivingLicense/Index','','行驾驶证查询','0000','1001',GETDATE(),18)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('3002','3000','1','../DrivingLicenseManagement/TempDrivingLicense/Index','','临时行驶证查询','0000','1001',GETDATE(),19)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)
                       values('4000','','0','','glyphicon glyphicon-list-alt','车牌号管理','0000','1001',GETDATE(),4)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('5000','','0','','glyphicon glyphicon-list-alt','事故管理','0000','1001',GETDATE(),5)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('5001','5000','1','../TrafficAccidentManagement/TrafficAccidentRegister/Index','','交通事故登记','0000','1001',GETDATE(),20)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('5002','5000','1','../TrafficAccidentManagement/TrafficAccidentQuery/Index','','交通事故查询','0000','1001',GETDATE(),21)
   

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('6000','','0','','glyphicon glyphicon-list-alt','消防管理','0000','1001',GETDATE(),6)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('6001','6000','1','../FireControlManagement/FireControlRegister/Index','','消防事故登记','0000','1001',GETDATE(),22)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('6002','6000','1','../FireControlManagement/FireControlQuery/Index','','消防事故查询','0000','1001',GETDATE(),23)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('6003','6000','1','../FireControlManagement/FireEquipmentRegister/Index','','消防设备登记','0000','1001',GETDATE(),24)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('6004','6000','1','../FireControlManagement/FireEquipmentQuery/Index','','消防设备查询','0000','1001',GETDATE(),25)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)
                       values('7000','','0','','glyphicon glyphicon-file','统计分析','0000','1001',GETDATE(),7)
   

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)
                       values('8000','','0','','glyphicon glyphicon-user','用户管理','0000','1001',GETDATE(),8)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)
   values('8001','8000','1','../Account/User/Index','','系统用户','0000','1001',GETDATE(),26)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)
   values('8002','8000','1','../Account/UserGroup/Index','','用户组','0000','1001',GETDATE(),27)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)
                       values('9000','','0','','glyphicon glyphicon-cog','系统设置','0000','1001',GETDATE(),9)
end
go