ALTER DATABASE shipperdb SET allow_snapshot_isolation ON;

ALTER DATABASE shipperdb SET read_committed_snapshot ON;


CREATE TABLE dbo.BlogUser (
  Id int IDENTITY,
  UserPassword nvarchar(40) NOT NULL,
  Name nvarchar(40) NOT NULL,
  Nick nvarchar(40) NOT NULL,
  PRIMARY KEY CLUSTERED (Id)
);


CREATE TABLE dbo.BlogPost (
  Id int IDENTITY,
  Title nvarchar(255) NOT NULL,
  Content ntext NULL,
  Created datetimeoffset NOT NULL,
  UserId int NOT NULL,
  PRIMARY KEY CLUSTERED (Id)
);


CREATE UNIQUE INDEX UK_BlogUser_Nick
  ON dbo.BlogUser (Nick)
  ON [PRIMARY]
  ;