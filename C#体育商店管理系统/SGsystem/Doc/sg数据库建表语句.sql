create table purchase
(
	purchase_id nvarchar(20) primary key,
	purchase_source	nvarchar(20),
	purchase_worker_id	nvarchar(20),
	purchase_data	nvarchar(20),
	purchase_name	nvarchar(20),
	purchase_unit	nvarchar(10),
	purchase_number	decimal(10,2),
	purchase_price	decimal(10,2),
	purchase_real_price	decimal(10,2),
	purchase_others	nvarchar(50)
);

create table sales
(
	sales_id	nvarchar(20)	primary key,
	sales_whereabouts	nvarchar(20),
	sales_worker_id	nvarchar(20),
	sales_data	nvarchar(20),
	sales_name	nvarchar(20),
	sales_unit	nvarchar(10),
	sales_number	decimal(10,2),
	sales_price	decimal(10,2),
	sales_real_price	decimal(10,2),
	sales_others	nvarchar(50)
)

create table warehouse¡¡(
	warehouse_id	int	primary key,
	warehouse_name	nvarchar(20),
	warehouse_price	decimal(10,2),
	warehouse_unit	nvarchar(20),
	warehouse_number	decimal(10,2),
	warehouse_others	nvarchar(50)	

)

create table receipt(
	receipt_id	nvarchar(20)	primary key,
	receipt_sales_id	nvarchar(20),
	receipt_worker_id	nvarchar(20),
	receipt_data	nvarchar(20),
	receipt_number	decimal(10,2),
	receipt_type	nvarchar(20),
	receipt_others	nvarchar(50),
	foreign key (receipt_sales_id) references sales (sales_id)
)

create table pay(
	pay_id	nvarchar(20)	primary key,
	pay_purchase_id	nvarchar(20),
	pay_worker_id	nvarchar(20),
	pay_data	nvarchar(20),
	pay_number	decimal(10,2),
	pay_type	nvarchar(20),
	pay_others	nvarchar(50)	
	foreign key (pay_purchase_id) references purchase (purchase_id)
)

create table funds(
	funds_id	int	primary key,
	funds_location	nvarchar(20),
	funds_number	decimal(10,2)	

)

create table users(
	users_name	nvarchar(20),
	users_password	nvarchar(20),
	users_id	nvarchar(20)	primary key,
	users_level	int

)