Create Table færdigheder
(
Id int Primary key
Navn VARCHAR
)


Create Table Aktivitet
(
AktivitetsId int Primary key,
AktivitetDato DateTime,
Int AktivitettypeId
Foreign Key (AktivitetType) References AktivitetType(AktivitetTypeId) 
)



Create Table Bruger
(
Id int primary key,
Navn VARCHAR,
Mail VARCHAR NOT NULL
)


Create Table Spejdere
(
Id int identity(1,1) primary key,
Navn VARCHAR(100) NOT NULL,
alder Int NOT NULL,
ErLeder BIT
)

