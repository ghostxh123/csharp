
drop table purchase;
create	table	purchase	(	
id                      int identity(1,1)   primary key	,	
purchase_id				nvarchar(20)		            	,	--	���������		--
purchase_source			nvarchar(20)						,	--	������Դ		--
purchase_worker_id		nvarchar(20)						,	--	����ְԱ		--
purchase_data			nvarchar(20)						,	--	��������		--
purchase_name			nvarchar(20)						,	--	��Ʒ����		--
purchase_unit			nvarchar(10)						,	--	��λ			--
purchase_number			decimal(10,2)						,	--	����			--
purchase_price			decimal(10,2)						,	--	����			--
purchase_real_price		decimal(10,2)						,	--	�ۿۼ�			--
purchase_others			nvarchar(50)						,	--	��ע			--
purchase_whole_price	decimal(10,2)							--	�ܼ�			--
)

drop table sales;
create	table	sales	(
id                      int identity(1,1)   primary key	,	
sales_id				nvarchar(20)		         		,	--	���۵����		--
sales_whereabouts		nvarchar(20)						,	--	����ȥ��		--
sales_worker_id			nvarchar(20)						,	--	����ְԱ		--
sales_data				nvarchar(20)						,	--	��������		--
sales_name				nvarchar(20)						,	--	��Ʒ����		--
sales_unit				nvarchar(10)						,	--	��λ			--
sales_number			decimal(10,2)						,	--	����			--
sales_price				decimal(10,2)						,	--	����			--
sales_real_price		decimal(10,2)						,	--	�ۿۼ�			--
sales_others			nvarchar(50)						,	--	��ע			--
sales_whole_price		decimal(10,2)							--	�ܼ�			--
)				

drop table recipt;
create	table	recipt	(
receipt_id				nvarchar(20)		primary key		,	--	�տ��		--
receipt_sales_id		nvarchar(20)						,	--	�տ�׵���	--
receipt_worker_id		nvarchar(20)						,	--	�տ�ְԱ		--
receipt_data			nvarchar(20)						,	--	�տ�����		--
receipt_number			decimal(10,2)						,	--	�տ���Ŀ		--
receipt_type			nvarchar(20)						,	--	�տʽ		--
receipt_others			nvarchar(50)							--	�տע		--
)
		
drop table funds;		
create	table	funds	(			
funds_id				int					primary key		,	--	id				--
funds_location			nvarchar(20)						,	--	�ʽ�洢λ��	--
funds_number			decimal(38,2)							--	�ʽ���Ŀ		--
)

drop table users;
create	table	users	(
users_workid			nvarchar(20)		primary key		,	--	ְԱid			--
users_password			nvarchar(20)						,	--	����			--
users_level				nvarchar(100)						,	--	Ȩ�޵ȼ�		--
users_name              nvarchar(20)                        ,	--	ְԱ����		--
users_sex				nvarchar(20)						,	--	ְԱ�Ա�		--
users_id				nvarchar(20)							--	ְԱ���֤��	--
)

--	�����˻���		--
drop table TGReturns;
create	table	TGReturns	(
id                      int identity(1,1)   primary key	,	
TGReturns_id			nvarchar(20)		            	,	--	�˻������		--
TGReturns_purchaseid	nvarchar(20)						,	--	�˻���Ӧ�Ľ�������	--
TGReturns_source		nvarchar(20)						,	--	�˻���Դ		--
TGReturns_worker_id		nvarchar(20)						,	--	�˻�ְԱ		--
TGReturns_data			nvarchar(20)						,	--	�˻�����		--
TGReturns_name			nvarchar(20)						,	--	��Ʒ����		--
TGreturns_unit			nvarchar(10)						,	--	��λ			--
TGReturns_number		decimal(10,2)						,	--	����			--
TGReturns_price			decimal(10,2)						,	--	����			--
TGReturns_others		nvarchar(50)							--	��ע			--
)

drop table warehouse;
create	table	warehouse	(
warehouse_id			int					primary key		,	--	id				--
warehouse_name			nvarchar(20)						,	--	��Ʒ��			--
warehouse_unit			nvarchar(10)						,	--	��λ			--
warehouse_number		decimal(10,2)						,	--	����			--
warehouse_price			decimal(10,2)						,	--	����			--
warehouse_others		nvarchar(50)							--	��ע			--
)

drop table pay;
create	table	pay	(
pay_id					nvarchar(20)		primary key		,	--	�����		--
pay_purchase_id			nvarchar(20)						,	--	����׵���	--
pay_worker_id			nvarchar(20)						,	--	����ְԱ		--
pay_data				nvarchar(20)						,	--	��������		--
pay_number				decimal(10,2)						,	--	������Ŀ		--
pay_type				nvarchar(20)						,	--	���ʽ		--
pay_others				nvarchar(50)							--	���ע		--
)

--	����������	--
drop table TKReturns;
create	table	TKReturns	(
id                      int identity(1,1)   primary key	,	
TKReturns_id			nvarchar(20)		        		,	--	�˻������		--
TKReturns_salesid		nvarchar(20)						,	--	�˻���Ӧ�����۵���	--
TKReturns_worker_id		nvarchar(20)						,	--	�˻�ְԱ		--
TKReturns_data			nvarchar(20)						,	--	�˻�����		--
TKReturns_name			nvarchar(20)						,	--	��Ʒ����		--
TKReturns_unit			nvarchar(10)						,	--	��λ			--
TKReturns_price			decimal(10,2)						,	--	����			--
TKReturns_number		decimal(10,2)						,	--	����			--
TKReturns_others		nvarchar(50)							--	��ע			--
)

drop table permission;
create table permission (
permission_id			int identity(1,1)	primary key		,	--	Ȩ�ޱ��		--
permission_na			nvarchar(20)						,	--	Ȩ������		--
permission_code			nvarchar(100)							--	Ȩ�ޱ���		--
)

/*
create database sgmanagement
drop table purchase;
drop table sales;
drop table recipt;
drop table funds;
drop table users;
drop table TGReturns;
drop table warehouse;
drop table pay;
drop table TKReturns;
drop table permission;
*/