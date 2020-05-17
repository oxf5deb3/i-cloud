--׼�ݳ���
if exists(select 1 from sysobjects where xtype='U' and id=OBJECT_ID('t_bd_permitted_car_type'))
begin
    truncate table t_bd_permitted_car_type
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WA','���Ϳͳ���B','ϵͳĬ��')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WB','���ͻ�����C.M','ϵͳĬ��')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WC','С������','ϵͳĬ��')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WD','�����ʽ����Ħ�г���E.L','ϵͳĬ��')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WE','����Ħ�г���F','ϵͳĬ��')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WF','���Ħ�г�','ϵͳĬ��')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WG','����������,����','ϵͳĬ��')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WH','С��������','ϵͳĬ��')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WK','�ַ�������','ϵͳĬ��')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WL','����ũ�����䳵','ϵͳĬ��')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WM','��ʽ����ר�û�е','ϵͳĬ��')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WN','�޹�糵','ϵͳĬ��')
	insert into t_bd_permitted_car_type(type_no,type_name,memo) values('WQ','��ƿ��','ϵͳĬ��')
end
go
if exists(select 1 from sysobjects where xtype='U' and id=OBJECT_ID('t_bd_region'))
begin
 truncate table t_bd_region
 insert into t_bd_region(region_no,region_name,memo) values('0001','����','ϵͳĬ��')
end
go





--��Դ����
if exists(select 1 from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_resource_type'))
begin
  truncate table t_sys_resource_type
  insert into t_sys_resource_type(id,type_name,type_desc) values('0000','Menu','�˵�')
  insert into t_sys_resource_type(id,type_name,type_desc) values('0001','Button','��ť')
end
go

--��Դ����
if exists(select 1 from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_res_type_operation'))
begin
   truncate table t_sys_res_type_operation
   insert into t_sys_res_type_operation(id,type_id,oper_code,oper_desc) values('0000','0000','Open','�ɼ�')
   insert into t_sys_res_type_operation(id,type_id,oper_code,oper_desc) values('0001','0001','Add','���')
   insert into t_sys_res_type_operation(id,type_id,oper_code,oper_desc) values('0002','0001','Edit','�༭')
   insert into t_sys_res_type_operation(id,type_id,oper_code,oper_desc) values('0003','0001','Delete','ɾ��')
   insert into t_sys_res_type_operation(id,type_id,oper_code,oper_desc) values('0004','0001','Export','����')
   insert into t_sys_res_type_operation(id,type_id,oper_code,oper_desc) values('0005','0001','Print','��ӡ')
end
go

--��Դ
if exists(select 1 from sysobjects where xtype='U' and id=OBJECT_ID('t_sys_resource'))
begin
   truncate table t_sys_resource
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('0000','','0','','glyphicon glyphicon-home','��������','0000','0000','1001',GETDATE(),0)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('0001','0000','1','../BaseData/PermittedCarType/Index','','׼�ݳ���','0000','0000','1001',GETDATE(),10)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('0002','0000','1','../BaseData/Region/Index','','������Ϣ','0000','0000','1001',GETDATE(),11)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('1000','','0','','glyphicon glyphicon-book','�Ǽ�����','0000','0000','1001',GETDATE(),1)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('1001','1000','1','../RegisterCenter/TempDriverLicenseRegister/Index','','��ʱ��ʻ֤�Ǽ�','0000','0000','1001',GETDATE(),12)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('1002','1000','1','../RegisterCenter/DriverLicenseRegister/Index','','��ʻ֤�Ǽ�','0000','0000','1001',GETDATE(),13)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('1003','1000','1','../RegisterCenter/TempDrivingLicenseRegister/Index','','��ʱ��ʻ֤�Ǽ�','0000','0000','1001',GETDATE(),14)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('1004','1000','1','../RegisterCenter/DrivingLicenseRegister/Index','','��ʻ֤�Ǽ�','0000','0000','1001',GETDATE(),15)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('2000','','0','','glyphicon glyphicon-book','��ʻ֤����','0000','0000','1001',GETDATE(),2)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('2001','2000','1','../DriverLicenseManagement/DriverLicense/Index','','��ʻ֤��ѯ','0000','0000','1001',GETDATE(),16)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('2002','2000','1','../DriverLicenseManagement/TempDriverLicense/Index','','��ʱ��ʻ֤��ѯ','0000','0000','1001',GETDATE(),17)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('3000','','0','','glyphicon glyphicon-book','��ʻ֤����','0000','0000','1001',GETDATE(),3)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('3001','3000','1','../DrivingLicenseManagement/DrivingLicense/Index','','��ʻ֤��ѯ','0000','0000','1001',GETDATE(),18)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('3002','3000','1','../DrivingLicenseManagement/TempDrivingLicense/Index','','��ʱ��ʻ֤��ѯ','0000','0000','1001',GETDATE(),19)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
                       values('4000','','0','','glyphicon glyphicon-list-alt','���ƺŹ���','0000','0000','1001',GETDATE(),4)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('4001','4000','1','../CarNumberManagement/TempCarNumber/Index','','��ʱ���ƺ�','0000','0000','1001',GETDATE(),20)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('4002','4000','1','../CarNumberManagement/CarNumber/Index','','��ʽ���ƺ�','0000','0000','1001',GETDATE(),21)


   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('5000','','0','','glyphicon glyphicon-list-alt','�¹ʹ���','0000','0000','1001',GETDATE(),5)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('5001','5000','1','../TrafficAccidentManagement/TrafficAccidentRegister/Index','','��ͨ�¹ʵǼ�','0000','0000','1001',GETDATE(),22)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('5002','5000','1','../TrafficAccidentManagement/TrafficAccidentQuery/Index','','��ͨ�¹ʲ�ѯ','0000','0000','1001',GETDATE(),23)
 
  
   

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6000','','0','','glyphicon glyphicon-list-alt','��������','0000','0000','1001',GETDATE(),6)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6001','6000','1','../FireControlManagement/FireControlRegister/Index','','�����¹ʵǼ�','0000','0000','1001',GETDATE(),24)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6002','6000','1','../FireControlManagement/FireControlQuery/Index','','�����¹ʲ�ѯ','0000','0000','1001',GETDATE(),25)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6003','6000','1','../FireControlManagement/FireEquipmentRegister/Index','','�����豸�Ǽ�','0000','0000','1001',GETDATE(),26)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6004','6000','1','../FireControlManagement/FireEquipmentQuery/Index','','�����豸��ѯ','0000','0000','1001',GETDATE(),27)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
                       values('7000','','0','','glyphicon glyphicon-file','ͳ�Ʒ���','0000','0000','1001',GETDATE(),7)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('7001','7000','1','../Statistical/DrivingLicense/Index','','��ʻ֤ͳ�����','0000','0000','1001',GETDATE(),28)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('7002','7000','1','../Statistical/trend/Index','','��ʻ֤��չ����ͳ��','0000','0000','1001',GETDATE(),29)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('7003','7000','1','../Statistical/VehicleLicenseS/Index','','��ʻ֤ͳ�����','0000','0000','1001',GETDATE(),30)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
                       values('8000','','0','','glyphicon glyphicon-user','�û�����','0000','0000','1001',GETDATE(),8)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('8001','8000','1','../Account/User/Index','','ϵͳ�û�','0000','0000','1001',GETDATE(),31)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('8002','8000','1','../Account/UserGroup/Index','','ϵͳ�û���','0000','0000','1001',GETDATE(),32)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('8003','8000','1','../Account/Role/Index','','ϵͳ��ɫ','0000','0000','1001',GETDATE(),33)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('8004','8000','1','../Account/RoleRes/Index','','��ɫȨ��','0000','0000','1001',GETDATE(),34)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
                       values('9000','','0','','glyphicon glyphicon-cog','ϵͳ��־','0000','0000','1001',GETDATE(),9)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('9001','9000','1','../SystemLog/LoginLog/Index','','��½��־','0000','0000','1001',GETDATE(),35)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
   values('9002','9000','1','../SystemLog/OperateLog/Index','','������־','0000','0000','1001',GETDATE(),36)
end
go
--��������ԱĬ��д��,�˺�:1001 ����:1001///////////////////////////////////////////////////////
if not exists(select 1 from t_sys_user where user_id='1001' and user_pwd='AA41D8FF38420ABE')
begin
  insert into t_sys_user(user_id,user_pwd,user_name,sex,age,tel,email,status,last_login_time,create_date)
values('1001','AA41D8FF38420ABE','��������Ա','0','28','15200790169','oxf5de98@163.com','0',null,getdate())
end
go
--д�볬�ܽ�ɫ
if not exists(select 1 from t_sys_role where role_name='ϵͳ����Ա')
begin
   insert into t_sys_role(role_name,create_id,create_date,status,memo) values('ϵͳ����Ա','1001',getdate(),'0','ϵͳĬ��')
end
if not exists(select 1 from t_sys_oper_role where role_id =(select top 1 id from t_sys_role where role_name='ϵͳ����Ա') and user_id='1001')
begin
   insert into t_sys_oper_role(role_id,user_id) select (select top 1 id from t_sys_role where role_name='ϵͳ����Ա') as role_id,'1001' as user_id
end
go
--д�볬��Ȩ��
if not exists(select 1 from t_sys_role_right where role_id=(select id from t_sys_role where role_name='ϵͳ����Ա'))
begin
   insert into t_sys_role_right(role_id,res_id,grant_id) select (select top 1 id from t_sys_role where role_name='ϵͳ����Ա') as role_id,id as res_id,'0000' as grant_id from t_sys_resource 
end
else
begin
  delete from t_sys_role_right where role_id=(select id from t_sys_role where role_name='ϵͳ����Ա')
  insert into t_sys_role_right(role_id,res_id,grant_id) select (select top 1 id from t_sys_role where role_name='ϵͳ����Ա') as role_id,id as res_id,'0000' as grant_id from t_sys_resource 
end
go
--��������ԱĬ��д��,�˺�:1001 ����:1001////////////////////////////////////////////////

--�˵��л�
if exists(select 1 from t_sys_resource where id='2002' and res_desc='��ʱ��ʻ֤��ѯ')
begin
  update t_sys_resource set res_uri='../DrivingLicenseManagement/DrivingLicense/Index',res_desc='��ʻ֤��ѯ' where id='2002'
end
go
if exists(select 1 from t_sys_resource where id='3001' and res_desc='�м�ʻ֤��ѯ')
begin
  update t_sys_resource set res_uri='../DriverLicenseManagement/TempDriverLicense/Index',res_desc='��ʱ��ʻ֤��ѯ' where id='3001'
end
go
if exists(select 1 from t_sys_resource where id='2000' and res_desc='��ʻ֤����')
begin
  update t_sys_resource set res_desc='��ʽ֤������' where id='2000'
end
go
if exists(select 1 from t_sys_resource where id='3000' and res_desc='��ʻ֤����')
begin
   update t_sys_resource set res_desc='��ʱ֤������' where id='3000'
end
go
--��ӡģ��
if not exists(select 1 from sysobjects where id=object_id('t_sys_print_template') and xtype='U')
create table t_sys_print_template(
  id decimal(16,0) identity(1,1),
  status char(1) not null,--0:Ĭ��ģ�� 1:�Զ���ģ��
  type int not null, --��ʾ��������ģ�壬����:��ʻ֤ģ�壬��ʻ֤ģ���
  oper_id varchar(10) not null,
  name nvarchar(50) not null,
  html text not null
  constraint pk_t_sys_print_template primary key clustered(id)
)
go
--��ʻ֤ģ��
insert into t_sys_print_template(status,type,oper_id,name,html)
  values('0',1,'1001','Ĭ��ģ��','<span class="easyui-draggable" style="position: absolute; left: 9px; top: 7px;">
            ����
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 85px; top: 7px;">
            W֤��
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 9px; top: 35px;">
            �Ա�
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 82.55px; top: 35px;">
            ������
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 137.1px; top: 35px;">
            ������
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 192.1px; top: 35px;">
            ������
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 9px; top: 63px;">
            ��λ��סַ
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 9px; top: 91px;">
            ������֤����-��
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 115.65px; top: 91px;">
            ������֤����-��
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 219.65px; top: 91px;">
            ������֤����-��
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 323.3px; top: 91px;">
            ׼�ݳ���
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 9px; top: 124px;">
            ��Ч������-��
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 104.1px; top: 124px;">
            ��Ч������-��
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 196.1px; top: 124px;">
            ��Ч������-��
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 286.388px; top: 124px;">
            ��Ч����ֹ-��
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 374.1px; top: 124px;">
            ��Ч����ֹ-��
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 465.1px; top: 124px;">
            ��Ч����ֹ-��
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 9px; top: 161px;">
            ����
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 91.1px; top: 161px;">
            W֤��
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 9px; top: 191px;">
            ����
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 191.1px; top: 285px;">
            ��ά��
        </span>')
go
--��ʻ֤ģ��
insert into t_sys_print_template(status,type,oper_id,name,html)
values('0',0,'1001','Ĭ��ģ��','    <span class="easyui-draggable" style="position: absolute; left: 11.3px; top: 197px;">
            ���ƺ���
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 90px; top: 11px;">
            ��������
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 12.3px; top: 41px;">
            ����
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 90px; top: 41px;">
            סַ
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 10.3px; top: 70px;">
            ��������
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 90px; top: 69px;">
            ������ɫ
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 11.3px; top: 97px;">
            ���ܺ�
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 11.3px; top: 130px;">
            ��֤����-��
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 90px; top: 130px;">
            ��֤����-��
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 171.65px; top: 130px;">
            ��֤����-��
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 11.3px; top: 11px;">
            ���ƺ���
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 90px; top: 197px;">
            ��������
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 11.3px; top: 236px;">
            �˶��ؿ�
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 11.3px; top: 277px;">
            ��ע
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 90px; top: 277px;">
            �����¼
        </span>
        <span class="easyui-draggable" style="position: absolute; left: 194.1px; top: 328px;">
            ��ά��
        </span>')
go
--20200324 Υ��
if exists(select 1 from sysobjects where xtype='U' and id=OBJECT_ID('t_bd_breakrules_type'))
begin
    truncate table t_bd_breakrules_type
	insert into t_bd_breakrules_type(name,punish_desc,memo,oper_id,oper_date) values('Υͣ','������200','ϵͳĬ��','1001',getdate())
end
go
if not exists(select 1 from t_sys_resource where id='6100')
begin
 insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6100','','0','','glyphicon glyphicon-list-alt','Υ�¹���','0000','0000','1001',GETDATE(),9)
end
go
if not exists(select 1 from t_sys_resource where id ='6101')
begin
 insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6101','6100','1','../BreakRules/BreakRuleType/Index','','Υ������','0000','0000','1001',GETDATE(),37)
end
if not exists(select 1 from t_sys_resource where id='6102')
begin
 insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)values('6102','6100','1','../BreakRules/BreakRuleQuery/Index','','Υ����Ϣ','0000','0000','1001',GETDATE(),38)
end
if not exists(select 1 from t_sys_resource where id='9003')
begin
 insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
 values('9003','9000','1','../SystemLog/SysNews/Index','','��Ѷ����','0000','0000','1001',GETDATE(),39)
end
if not exists(select 1 from t_sys_resource where id='8005')
begin
 insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,res_type_oper_id,create_id,create_date,sort_code)
 values('8005','8000','1','../Account/OutterUser/Index','','�ⲿ��Ա','0000','0000','1001',GETDATE(),40)
end
--�ֶ��޸�
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