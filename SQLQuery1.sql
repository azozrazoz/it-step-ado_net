USE MARKET
create table [Crypto]
(
	[Id] int not null primary key,
	[Price] float not null,
	[CreateOn] datetime not null,
	[UpdateOn] datetime not null,
	[DeleteOn] datetime not null,	
	[VersionRow] int not null,
	[_Name] nvarchar not null
);
create table [Wallet]
(
	[ID] int not null primary key,
	[ClientId] int not null,
	[CryptoId] int not null foreign key references Crypto(ID),
	[CryptoAmount] float not null,
	[CreateOn] datetime not null,
	[UpdateOn] datetime not null,
	[DeleteOn] datetime not null,
	[IsDeleted] bit not null
);
create table [ClientEntity]
(
	[ID] int not null primary key,
	[FullName] nvarchar(90) not null,	
	[CreateOn] datetime not null,
	[UpdateOn] datetime,
	[DeleteOn] datetime,
	[IsDeleted] bit not null
);
create table [Transaction]
(
	[ID] int not null primary key,
	[CreateOn] datetime not null,
	[FromClientId] int foreign key references ClientEntity(ID),
	[ToClientId] int foreign key references ClientEntity(ID),
	[CryptoId] int foreign key references Crypto(Id),
	[Sum] float not null,
	[FromWalletId] int not null,
	[ToWalletId] int not null,
);
create table [ConvertCrypto]
(
	[ID] int not null primary key,
	[ClientId] int not null foreign key references ClientEntity(ID),
	[FromCryptoId] int not null foreign key references Crypto(ID),
	[ToCryptoId] int not null foreign key references Crypto(ID),
	[FromSum] float not null,
	[ToSum] float not null,	
	[FromWalletId] int not null,
	[ToWalletId] int not null,
);