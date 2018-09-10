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
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('0000','','0','','glyphicon glyphicon-home','��������','0000','1001',GETDATE(),0)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('0001','0000','1','../BaseData/PermittedCarType/Index','','׼�ݳ���','0000','1001',GETDATE(),10)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('0002','0000','1','../BaseData/Region/Index','','������Ϣ','0000','1001',GETDATE(),11)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('1000','','0','','glyphicon glyphicon-book','�Ǽ�����','0000','1001',GETDATE(),1)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('1001','1000','1','../RegisterCenter/TempDriverLicenseRegister/Index','','��ʱ��ʻ֤�Ǽ�','0000','1001',GETDATE(),12)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('1002','1000','1','../RegisterCenter/DriverLicenseRegister/Index','','��ʻ֤�Ǽ�','0000','1001',GETDATE(),13)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('1003','1000','1','../RegisterCenter/TempDrivingLicenseRegister/Index','','��ʱ��ʻ֤�Ǽ�','0000','1001',GETDATE(),14)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('1004','1000','1','../RegisterCenter/DrivingLicenseRegister/Index','','��ʻ֤�Ǽ�','0000','1001',GETDATE(),15)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('2000','','0','','glyphicon glyphicon-book','��ʻ֤����','0000','1001',GETDATE(),2)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('2001','2000','1','../DriverLicenseManagement/DriverLicense/Index','','��ʻ֤��ѯ','0000','1001',GETDATE(),16)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('2002','2000','1','../DriverLicenseManagement/TempDriverLicense/Index','','��ʱ��ʻ֤��ѯ','0000','1001',GETDATE(),17)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('3000','','0','','glyphicon glyphicon-book','��ʻ֤����','0000','1001',GETDATE(),3)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('3001','3000','1','../DrivingLicenseManagement/DrivingLicense/Index','','�м�ʻ֤��ѯ','0000','1001',GETDATE(),18)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('3002','3000','1','../DrivingLicenseManagement/TempDrivingLicense/Index','','��ʱ��ʻ֤��ѯ','0000','1001',GETDATE(),19)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)
                       values('4000','','0','','glyphicon glyphicon-list-alt','���ƺŹ���','0000','1001',GETDATE(),4)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('5000','','0','','glyphicon glyphicon-list-alt','�¹ʹ���','0000','1001',GETDATE(),5)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('5001','5000','1','../TrafficAccidentManagement/TrafficAccidentRegister/Index','','��ͨ�¹ʵǼ�','0000','1001',GETDATE(),20)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('5002','5000','1','../TrafficAccidentManagement/TrafficAccidentQuery/Index','','��ͨ�¹ʲ�ѯ','0000','1001',GETDATE(),21)
   

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('6000','','0','','glyphicon glyphicon-list-alt','��������','0000','1001',GETDATE(),6)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('6001','6000','1','../FireControlManagement/FireControlRegister/Index','','�����¹ʵǼ�','0000','1001',GETDATE(),22)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('6002','6000','1','../FireControlManagement/FireControlQuery/Index','','�����¹ʲ�ѯ','0000','1001',GETDATE(),23)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('6003','6000','1','../FireControlManagement/FireEquipmentRegister/Index','','�����豸�Ǽ�','0000','1001',GETDATE(),24)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)values('6004','6000','1','../FireControlManagement/FireEquipmentQuery/Index','','�����豸��ѯ','0000','1001',GETDATE(),25)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)
                       values('7000','','0','','glyphicon glyphicon-file','ͳ�Ʒ���','0000','1001',GETDATE(),7)
   

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)
                       values('8000','','0','','glyphicon glyphicon-user','�û�����','0000','1001',GETDATE(),8)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)
   values('8001','8000','1','../Account/User/Index','','ϵͳ�û�','0000','1001',GETDATE(),26)
   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)
   values('8002','8000','1','../Account/UserGroup/Index','','�û���','0000','1001',GETDATE(),27)

   insert into t_sys_resource(id,pid,level,res_uri,res_img,res_desc,res_type_id,create_id,create_date,sort_code)
                       values('9000','','0','','glyphicon glyphicon-cog','ϵͳ����','0000','1001',GETDATE(),9)
end
go