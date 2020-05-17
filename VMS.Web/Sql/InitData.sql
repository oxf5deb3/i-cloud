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
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('0000','','0','','glyphicon glyphicon-home','基础资料','0000','0000','1001',GETDATE(),0)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('0001','0000','1','../BaseData/PermittedCarType/Index','','准驾车型','0000','0000','1001',GETDATE(),10)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('0002','0000','1','../BaseData/Region/Index','','区域信息','0000','0000','1001',GETDATE(),11)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('1000','','0','','glyphicon glyphicon-book','登记中心','0000','0000','1001',GETDATE(),1)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('1001','1000','1','../RegisterCenter/TempDriverLicenseRegister/Index','','临时驾驶证登记','0000','0000','1001',GETDATE(),12)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('1002','1000','1','../RegisterCenter/DriverLicenseRegister/Index','','驾驶证登记','0000','0000','1001',GETDATE(),13)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('1003','1000','1','../RegisterCenter/TempDrivingLicenseRegister/Index','','临时行驶证登记','0000','0000','1001',GETDATE(),14)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('1004','1000','1','../RegisterCenter/DrivingLicenseRegister/Index','','行驶证登记','0000','0000','1001',GETDATE(),15)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('2000','','0','','glyphicon glyphicon-book','驾驶证管理','0000','0000','1001',GETDATE(),2)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('2001','2000','1','../DriverLicenseManagement/DriverLicense/Index','','驾驶证查询','0000','0000','1001',GETDATE(),16)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('2002','2000','1','../DriverLicenseManagement/TempDriverLicense/Index','','临时驾驶证查询','0000','0000','1001',GETDATE(),17)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('3000','','0','','glyphicon glyphicon-book','行驶证管理','0000','0000','1001',GETDATE(),3)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('3001','3000','1','../DrivingLicenseManagement/DrivingLicense/Index','','行驶证查询','0000','0000','1001',GETDATE(),18)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('3002','3000','1','../DrivingLicenseManagement/TempDrivingLicense/Index','','临时行驶证查询','0000','0000','1001',GETDATE(),19)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
                       values('4000','','0','','glyphicon glyphicon-list-alt','车牌号管理','0000','0000','1001',GETDATE(),4)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('4001','4000','1','../CarNumberManagement/TempCarNumber/Index','','临时车牌号','0000','0000','1001',GETDATE(),20)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('4002','4000','1','../CarNumberManagement/CarNumber/Index','','正式车牌号','0000','0000','1001',GETDATE(),21)


   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('5000','','0','','glyphicon glyphicon-list-alt','事故管理','0000','0000','1001',GETDATE(),5)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('5001','5000','1','../TrafficAccidentManagement/TrafficAccidentRegister/Index','','交通事故登记','0000','0000','1001',GETDATE(),22)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('5002','5000','1','../TrafficAccidentManagement/TrafficAccidentQuery/Index','','交通事故查询','0000','0000','1001',GETDATE(),23)
 
  
   

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6000','','0','','glyphicon glyphicon-list-alt','消防管理','0000','0000','1001',GETDATE(),6)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6001','6000','1','../FireControlManagement/FireControlRegister/Index','','消防事故登记','0000','0000','1001',GETDATE(),24)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6002','6000','1','../FireControlManagement/FireControlQuery/Index','','消防事故查询','0000','0000','1001',GETDATE(),25)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6003','6000','1','../FireControlManagement/FireEquipmentRegister/Index','','消防设备登记','0000','0000','1001',GETDATE(),26)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6004','6000','1','../FireControlManagement/FireEquipmentQuery/Index','','消防设备查询','0000','0000','1001',GETDATE(),27)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
                       values('7000','','0','','glyphicon glyphicon-file','统计分析','0000','0000','1001',GETDATE(),7)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('7001','7000','1','../Statistical/DrivingLicense/Index','','驾驶证统计情况','0000','0000','1001',GETDATE(),28)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('7002','7000','1','../Statistical/trend/Index','','驾驶证发展趋势统计','0000','0000','1001',GETDATE(),29)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('7003','7000','1','../Statistical/VehicleLicenseS/Index','','行驶证统计情况','0000','0000','1001',GETDATE(),30)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
                       values('8000','','0','','glyphicon glyphicon-user','用户管理','0000','0000','1001',GETDATE(),8)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('8001','8000','1','../Account/User/Index','','系统用户','0000','0000','1001',GETDATE(),31)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('8002','8000','1','../Account/UserGroup/Index','','系统用户组','0000','0000','1001',GETDATE(),32)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('8003','8000','1','../Account/Role/Index','','系统角色','0000','0000','1001',GETDATE(),33)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('8004','8000','1','../Account/RoleRes/Index','','角色权限','0000','0000','1001',GETDATE(),34)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
                       values('9000','','0','','glyphicon glyphicon-cog','系统日志','0000','0000','1001',GETDATE(),9)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('9001','9000','1','../SystemLog/LoginLog/Index','','登陆日志','0000','0000','1001',GETDATE(),35)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('9002','9000','1','../SystemLog/OperateLog/Index','','操作日志','0000','0000','1001',GETDATE(),36)
end
go
--超级管理员默认写入,账号:1001 密码:1001///////////////////////////////////////////////////////
if not exists(select 1 from t_sys_user where user_id='1001' and user_pwd='AA41D8FF38420ABE')
begin
  insert into t_sys_user(user_id,user_pwd,user_name,sex,age,tel,email,status,last_login_time,create_date)
values('1001','AA41D8FF38420ABE','超级管理员','0','28','15200790169','oxf5de98@163.com','0',null,getdate())
end
go
--写入超管角色
if not exists(select 1 from t_sys_role where role_name='系统管理员')
begin
   insert into t_sys_role(role_name,create_id,create_date,status,memo) values('系统管理员','1001',getdate(),'0','系统默认')
end
if not exists(select 1 from t_sys_oper_role where role_id =(select top 1 id from t_sys_role where role_name='系统管理员') and user_id='1001')
begin
   insert into t_sys_oper_role(role_id,user_id) select (select top 1 id from t_sys_role where role_name='系统管理员') as role_id,'1001' as user_id
end
go
--写入超管权限
if not exists(select 1 from t_sys_role_right where role_id=(select id from t_sys_role where role_name='系统管理员'))
begin
   insert into t_sys_role_right(role_id,res_id,grant_id) select (select top 1 id from t_sys_role where role_name='系统管理员') as role_id,id as res_id,'0000' as grant_id from t_sys_resource 
end
else
begin
  delete from t_sys_role_right where role_id=(select id from t_sys_role where role_name='系统管理员')
  insert into t_sys_role_right(role_id,res_id,grant_id) select (select top 1 id from t_sys_role where role_name='系统管理员') as role_id,id as res_id,'0000' as grant_id from t_sys_resource 
end
go
--超级管理员默认写入,账号:1001 密码:1001////////////////////////////////////////////////

--菜单切换
if exists(select 1 from t_sys_resource where id='2002' and res_desc='临时驾驶证查询')
begin
  update t_sys_resource set res_uri='../DrivingLicenseManagement/DrivingLicense/Index',res_desc='行驶证查询' where id='2002'
end
go
if exists(select 1 from t_sys_resource where id='3001' and res_desc='行驾驶证查询')
begin
  update t_sys_resource set res_uri='../DriverLicenseManagement/TempDriverLicense/Index',res_desc='临时驾驶证查询' where id='3001'
end
go
if exists(select 1 from t_sys_resource where id='2000' and res_desc='驾驶证管理')
begin
  update t_sys_resource set res_desc='正式证件管理' where id='2000'
end
go
if exists(select 1 from t_sys_resource where id='3000' and res_desc='行驶证管理')
begin
   update t_sys_resource set res_desc='临时证件管理' where id='3000'
end
go
--打印模板
if not exists(select 1 from sysobjects where id=object_id('t_sys_print_template') and xtype='U')
create table t_sys_print_template(
  id decimal(16,0) identity(1,1),
  status char(1) not null,--0:默认模板 1:自定义模板
  type int not null, --表示那种类型模板，例如:驾驶证模板，行驶证模板等
  oper_id varchar(10) not null,
  name nvarchar(50) not null,
  html text not null
  constraint pk_t_sys_print_template primary key clustered(id)
)
go
--驾驶证模板
insert into t_sys_print_template(status,type,oper_id,name,html)
  values('0',1,'1001','默认模板','<span class="easyui-draggable" style="position: absolute; left: 9px; top: 7px;">
            姓名
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 85px; top: 7px;">
            W证号
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 9px; top: 35px;">
            性别
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 82.55px; top: 35px;">
            出生年
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 137.1px; top: 35px;">
            出生月
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 192.1px; top: 35px;">
            出生日
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 9px; top: 63px;">
            单位或住址
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 9px; top: 91px;">
            初次领证日期-年
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 115.65px; top: 91px;">
            初次领证日期-月
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 219.65px; top: 91px;">
            初次领证日期-日
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 323.3px; top: 91px;">
            准驾车型
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 9px; top: 124px;">
            有效日期起-年
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 104.1px; top: 124px;">
            有效日期起-月
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 196.1px; top: 124px;">
            有效日期起-日
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 286.388px; top: 124px;">
            有效日期止-年
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 374.1px; top: 124px;">
            有效日期止-月
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 465.1px; top: 124px;">
            有效日期止-日
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 9px; top: 161px;">
            姓名
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 91.1px; top: 161px;">
            W证号
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 9px; top: 191px;">
            审验
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 191.1px; top: 285px;">
            二维码
        </span>')
go
--行驶证模板
insert into t_sys_print_template(status,type,oper_id,name,html)
values('0',0,'1001','默认模板','    <span class="easyui-draggable" style="position: absolute; left: 11.3px; top: 197px;">
            车牌号码
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 90px; top: 11px;">
            车辆类型
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 12.3px; top: 41px;">
            车主
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 90px; top: 41px;">
            住址
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 10.3px; top: 70px;">
            发动机号
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 90px; top: 69px;">
            车辆颜色
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 11.3px; top: 97px;">
            车架号
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 11.3px; top: 130px;">
            发证日期-年
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 90px; top: 130px;">
            发证日期-月
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 171.65px; top: 130px;">
            发证日期-日
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 11.3px; top: 11px;">
            车牌号码
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 90px; top: 197px;">
            车辆类型
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 11.3px; top: 236px;">
            核定载客
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 11.3px; top: 277px;">
            备注
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 90px; top: 277px;">
            检验记录
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 194.1px; top: 328px;">
            二维码
        </span>')
go
--20200324 违章
if exists(select 1 from sysobjects where xtype='U' and id=OBJECT_ID('t_bd_breakrules_type'))
begin
    truncate table t_bd_breakrules_type
	insert into t_bd_breakrules_type(name,punish_desc,memo,oper_id,oper_date) values('违停','处罚金200','系统默认','1001',getdate())
end
go
if not exists(select 1 from t_sys_resource where id='6100')
begin
 insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6100','','0','','glyphicon glyphicon-list-alt','违章管理','0000','0000','1001',GETDATE(),9)
end
go
if not exists(select 1 from t_sys_resource where id ='6101')
begin
 insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6101','6100','1','../BreakRules/BreakRuleType/Index','','违章类型','0000','0000','1001',GETDATE(),37)
end
if not exists(select 1 from t_sys_resource where id='6102')
begin
 insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6102','6100','1','../BreakRules/BreakRuleQuery/Index','','违章信息','0000','0000','1001',GETDATE(),38)
end
if not exists(select 1 from t_sys_resource where id='9003')
begin
 insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
 values('9003','9000','1','../SystemLog/SysNews/Index','','资讯管理','0000','0000','1001',GETDATE(),39)
end
if not exists(select 1 from t_sys_resource where id='8005')
begin
 insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
 values('8005','8000','1','../Account/OutterUser/Index','','外部人员','0000','0000','1001',GETDATE(),40)
end
--字段修改
if exists(select 1 from syscolumns where name='tel' and id=OBJECT_ID('t_sys_user'))
begin
   alter table t_sys_user alter column tel varchar(20) null
end
go
if not exists(select 1 from sysobjects where xtype='D' and OBJECT_NAME(parent_obj)='t_sys_user' and name='df_sex')
begin
   alter table t_sys_user add constraint df_sex default('0') for sex with values 
end
go
if not exists(select 1 from sysobjects where xtype='D' and OBJECT_NAME(parent_obj)='t_sys_user' and name='df_age')
begin
   alter table t_sys_user add constraint df_age default('0') for age with values 
end
go
if not exists(select 1 from syscolumns where name='engine_no' and OBJECT_NAME(id)='t_bd_breakrules')
begin
   alter table t_bd_breakrules add engine_no varchar(50) null
end
if  exists(select 1 from syscolumns where name='accident_desc' and OBJECT_NAME(id)='t_accident_records')
begin
   alter table t_accident_records alter column accident_desc varchar(1000) null
end
if not exists(select 1 from syscolumns where name='first_party_car_no' and OBJECT_NAME(id)='t_accident_records')
begin
   alter table t_accident_records add first_party_car_no varchar(50) null
end
if not exists(select 1 from syscolumns where name='second_party_car_no' and OBJECT_NAME(id)='t_accident_records')
begin
   alter table t_accident_records add second_party_car_no varchar(50) null
end

if  exists(select 1 from syscolumns where name='happen_date' and OBJECT_NAME(id)='t_accident_records')
begin
   alter table t_accident_records alter column happen_date datetime null
end
if  exists(select 1 from syscolumns where name='first_party_man' and OBJECT_NAME(id)='t_accident_records')
begin
   alter table t_accident_records alter column first_party_man varchar(50) null
end
if  exists(select 1 from syscolumns where name='first_party_addr' and OBJECT_NAME(id)='t_accident_records')
begin
   alter table t_accident_records alter column first_party_addr varchar(200) null
end
if  exists(select 1 from syscolumns where name='second_party_man' and OBJECT_NAME(id)='t_accident_records')
begin
   alter table t_accident_records alter column second_party_man varchar(50) null
end
if  exists(select 1 from syscolumns where name='second_party_addr' and OBJECT_NAME(id)='t_accident_records')
begin
   alter table t_accident_records alter column second_party_addr varchar(200) null
end
if  exists(select 1 from syscolumns where name='mediation_unit' and OBJECT_NAME(id)='t_accident_records')
begin
   alter table t_accident_records alter column mediation_unit varchar(200) null
end
if  exists(select 1 from syscolumns where name='mediation_date' and OBJECT_NAME(id)='t_accident_records')
begin
   alter table t_accident_records alter column mediation_date datetime null
end
if  exists(select 1 from syscolumns where name='draw_recorder' and OBJECT_NAME(id)='t_accident_records')
begin
   alter table t_accident_records alter column draw_recorder varchar(50) null
end
if  exists(select 1 from syscolumns where name='accident_mediator' and OBJECT_NAME(id)='t_accident_records')
begin
   alter table t_accident_records alter column accident_mediator varchar(50) null
end