--app�˻�ȡȨ��
if exists(select 1 from sysobjects where xtype='P' and id =OBJECT_ID('pr_app_get_menu_rights'))
begin
  drop procedure pr_app_get_menu_rights
end
go
create procedure pr_app_get_menu_rights
@oper_id varchar(20)
as 
begin
declare @tmp table   --���������
(
id int identity(1,1),    --�ֶ� ����Ͳ��������������һһ��Ӧ
menu_id varchar(10),
rights char(1),
pos int
)
insert into @tmp
select '1001' as menu_id,1 as rights,1 as pos --��ʱ��ʻ֤�Ǽ�
union
select '1002' as menu_id,1 as rights,2 as pos --��ʻ֤�Ǽ�
union
select '1003' as menu_id,1 as rights,3 as pos --��ʱ��ʻ֤�Ǽ�
union
select '1004' as menu_id,1 as rights,4 as pos --��ʻ֤�Ǽ�
union						    
select '2001' as menu_id,1 as rights,5 as pos --��ʻ֤��ѯ
union						    
select '2002' as menu_id,1 as rights,6 as pos --��ʻ֤��ѯ
union						    
select '3001' as menu_id,1 as rights,7 as pos --��ʱ��ʻ֤��ѯ
union						   
select '3002' as menu_id,1 as rights,8 as pos --��ʱ��ʻ֤��ѯ
union						    
select '4001' as menu_id,1 as rights,9 as pos --��ʱ���ƺ�
union						    
select '4002' as menu_id,1 as rights,10 as pos --��ʽ���ƺ�
union						    
select '5001' as menu_id,1 as rights,11 as pos --��ͨ�¹ʵǼ�
union						    
select '5002' as menu_id,1 as rights,12 as pos --��ͨ�¹ʲ�ѯ
union						    
select '6001' as menu_id,1 as rights,13 as pos --�����¹ʵǼ�
union						    
select '6002' as menu_id,1 as rights,14 as pos --�����¹ʲ�ѯ
union						    
select '6003' as menu_id,1 as rights,15 as pos --�����豸�Ǽ�
union						    
select '6004' as menu_id,1 as rights,16 as pos --�����豸��ѯ
union						    
select '6101' as menu_id,1 as rights,17 as pos --Υ������
union						    
select '6102' as menu_id,1 as rights,18 as pos --Υ����Ϣ
union						    
select '10002' as menu_id,1 as rights,19 as pos --�жӹ�����¼
union						    
select '10003' as menu_id,1 as rights,20 as pos --��Ѻ��ѯ
union						    
select '10004' as menu_id,1 as rights,21 as pos --��������ѯ

if exists(select * from t_sys_user where user_id=@oper_id and user_type='1')
begin
   ;with det as (
    select t.menu_id,(case when isnull(t1.id,'')='' then '0' else '1' end) as rights,t.pos
    from @tmp as t
	left join (
	  select '1001' as id
	  union
	  select '1002' as id
	 ) as t1 on t.menu_id=t1.id
	)
select (select ''+rights from det order by det.pos FOR XML PATH(''))
end
else
begin
 print 1
   ;with det as (
    select t.menu_id,(case when isnull(t1.id,'')='' then '0' else '1' end) as rights,t.pos
    from  @tmp as t
    left join 
    (
           select d.id,d.res_desc
           from t_sys_user a 
           left join t_sys_oper_role b on a.user_id=b.user_id 
           left join t_sys_role_right c on b.role_id=c.role_id 
           left join t_sys_resource d on c.res_id = d.id 
           left join t_sys_resource_type e on d.res_type_id=e.id 
           where a.user_id=@oper_id and e.type_name='Menu'and isnull(d.pid,'')!='' --and d.id like '[1-6]%' 
           union  
    	   select i.id,i.res_desc
           from t_sys_user_group f  
           left join t_sys_group_role g on f.group_id=g.group_id  
           left join t_sys_role_right h on g.role_id=h.role_id  
           left join t_sys_resource i on h.res_id = i.id  
           left join t_sys_resource_type j on i.res_type_id=j.id  
           where f.user_id=@oper_id and j.type_name='Menu'and isnull(i.pid,'')!='' --and i.id like '[1-6]%'
    ) as t1 on t.menu_id = t1.id
)
select (select ''+rights from det order by det.pos FOR XML PATH(''))
end
end
go
