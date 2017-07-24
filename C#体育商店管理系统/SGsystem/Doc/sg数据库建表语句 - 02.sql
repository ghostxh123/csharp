
drop table purchase;
create	table	purchase	(	
id                      int identity(1,1)   primary key	,	
purchase_id				nvarchar(20)		            	,	--	进货单编号		--
purchase_source			nvarchar(20)						,	--	进货来源		--
purchase_worker_id		nvarchar(20)						,	--	进货职员		--
purchase_data			nvarchar(20)						,	--	进货日期		--
purchase_name			nvarchar(20)						,	--	商品名称		--
purchase_unit			nvarchar(10)						,	--	单位			--
purchase_number			decimal(10,2)						,	--	数量			--
purchase_price			decimal(10,2)						,	--	单价			--
purchase_real_price		decimal(10,2)						,	--	折扣价			--
purchase_others			nvarchar(50)						,	--	备注			--
purchase_whole_price	decimal(10,2)							--	总价			--
)

drop table sales;
create	table	sales	(
id                      int identity(1,1)   primary key	,	
sales_id				nvarchar(20)		         		,	--	销售单编号		--
sales_whereabouts		nvarchar(20)						,	--	销货去向		--
sales_worker_id			nvarchar(20)						,	--	销货职员		--
sales_data				nvarchar(20)						,	--	销货日期		--
sales_name				nvarchar(20)						,	--	商品名称		--
sales_unit				nvarchar(10)						,	--	单位			--
sales_number			decimal(10,2)						,	--	数量			--
sales_price				decimal(10,2)						,	--	单价			--
sales_real_price		decimal(10,2)						,	--	折扣价			--
sales_others			nvarchar(50)						,	--	备注			--
sales_whole_price		decimal(10,2)							--	总价			--
)				

drop table recipt;
create	table	recipt	(
receipt_id				nvarchar(20)		primary key		,	--	收款单号		--
receipt_sales_id		nvarchar(20)						,	--	收款交易单号	--
receipt_worker_id		nvarchar(20)						,	--	收款职员		--
receipt_data			nvarchar(20)						,	--	收款日期		--
receipt_number			decimal(10,2)						,	--	收款数目		--
receipt_type			nvarchar(20)						,	--	收款方式		--
receipt_others			nvarchar(50)							--	收款备注		--
)
		
drop table funds;		
create	table	funds	(			
funds_id				int					primary key		,	--	id				--
funds_location			nvarchar(20)						,	--	资金存储位置	--
funds_number			decimal(38,2)							--	资金数目		--
)

drop table users;
create	table	users	(
users_workid			nvarchar(20)		primary key		,	--	职员id			--
users_password			nvarchar(20)						,	--	密码			--
users_level				nvarchar(100)						,	--	权限等级		--
users_name              nvarchar(20)                        ,	--	职员姓名		--
users_sex				nvarchar(20)						,	--	职员性别		--
users_id				nvarchar(20)							--	职员身份证号	--
)

--	进货退货单		--
drop table TGReturns;
create	table	TGReturns	(
id                      int identity(1,1)   primary key	,	
TGReturns_id			nvarchar(20)		            	,	--	退货单编号		--
TGReturns_purchaseid	nvarchar(20)						,	--	退货对应的进货单号	--
TGReturns_source		nvarchar(20)						,	--	退货来源		--
TGReturns_worker_id		nvarchar(20)						,	--	退货职员		--
TGReturns_data			nvarchar(20)						,	--	退货日期		--
TGReturns_name			nvarchar(20)						,	--	商品名称		--
TGreturns_unit			nvarchar(10)						,	--	单位			--
TGReturns_number		decimal(10,2)						,	--	数量			--
TGReturns_price			decimal(10,2)						,	--	单价			--
TGReturns_others		nvarchar(50)							--	备注			--
)

drop table warehouse;
create	table	warehouse	(
warehouse_id			int					primary key		,	--	id				--
warehouse_name			nvarchar(20)						,	--	商品名			--
warehouse_unit			nvarchar(10)						,	--	单位			--
warehouse_number		decimal(10,2)						,	--	数量			--
warehouse_price			decimal(10,2)						,	--	单价			--
warehouse_others		nvarchar(50)							--	备注			--
)

drop table pay;
create	table	pay	(
pay_id					nvarchar(20)		primary key		,	--	付款单号		--
pay_purchase_id			nvarchar(20)						,	--	付款交易单号	--
pay_worker_id			nvarchar(20)						,	--	付款职员		--
pay_data				nvarchar(20)						,	--	付款日期		--
pay_number				decimal(10,2)						,	--	付款数目		--
pay_type				nvarchar(20)						,	--	付款方式		--
pay_others				nvarchar(50)							--	付款备注		--
)

--	销货进货单	--
drop table TKReturns;
create	table	TKReturns	(
id                      int identity(1,1)   primary key	,	
TKReturns_id			nvarchar(20)		        		,	--	退货单编号		--
TKReturns_salesid		nvarchar(20)						,	--	退货对应的销售单号	--
TKReturns_worker_id		nvarchar(20)						,	--	退货职员		--
TKReturns_data			nvarchar(20)						,	--	退货日期		--
TKReturns_name			nvarchar(20)						,	--	商品名称		--
TKReturns_unit			nvarchar(10)						,	--	单位			--
TKReturns_price			decimal(10,2)						,	--	单价			--
TKReturns_number		decimal(10,2)						,	--	数量			--
TKReturns_others		nvarchar(50)							--	备注			--
)

drop table permission;
create table permission (
permission_id			int identity(1,1)	primary key		,	--	权限编号		--
permission_na			nvarchar(20)						,	--	权限名称		--
permission_code			nvarchar(100)							--	权限编码		--
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