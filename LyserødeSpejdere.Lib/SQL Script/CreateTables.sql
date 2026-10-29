-- slet tabellerne, hvis de findes, så scriptet kan køres igen
DROP TABLE IF EXISTS AktivitetTypeKrav;
DROP TABLE IF EXISTS BrugerFaerdighed;
DROP TABLE IF EXISTS AktivitetLeder;
DROP TABLE IF EXISTS Tilmelding;
DROP TABLE IF EXISTS Aktivitet;
DROP TABLE IF EXISTS AktivitetType;
DROP TABLE IF EXISTS Faerdighed;
DROP TABLE IF EXISTS Bruger;


-- Login for både spejdere og ledere

CREATE TABLE Bruger(
    
    BrugerId    int IDENTITY (1,1) PRIMARY KEY,
    Navn        varchar(255) NOT NULL,
    Mail        varchar(255) NOT NULL UNIQUE,
    Brugernavn  varchar(255) NOT NULL UNIQUE,
    Adgangskode varchar(255) NOT NULL,
    Foedselsdato date NOT NULL,
    ErLeder     bit NOT NULL DEFAULT 0
);

-- Tegn/Færdigheder, fx "Førstehjælp"

CREATE TABLE Faerdighed (
    
    FaerdighedId int IDENTITY (1,1) PRIMARY KEY,
    Navn    varchar(255) NOT NULL UNIQUE 
);

-- HVAD en aktivitet er, fx "Ridning" og dens regler 

CREATE TABLE AktivitetType(
    
    AktivitetTypeId int IDENTITY (1,1) PRIMARY KEY,
    Navn        varchar(255) NOT NULL,
    Beskrivelse varchar(255),
    MaxDeltagere int,
    MinAlder    int,
    ImagePath VarChar(255)
);

-- HVORNÅR og HVOR en aktivitet sker

CREATE TABLE Aktivitet(
    
    AktivitetId     int IDENTITY (1,1) PRIMARY KEY,
    AktivitetTypeId int NOT NULL,
    StartTid datetime NOT NULL,
    SlutTid datetime NOT NULL,
    Sted    varchar(255),
    FOREIGN KEY (AktivitetTypeId) REFERENCES AktivitetType(AktivitetTypeId)
);

-- Spejder tilmeldt en aktivitet 

CREATE TABLE Tilmelding (
    
    BrugerId int NOT NULL,
    AktivitetId int NOT NULL,
    PRIMARY KEY (BrugerId, AktivitetId),
    FOREIGN KEY (BrugerId) REFERENCES Bruger (BrugerId),
    FOREIGN KEY (AktivitetId) REFERENCES Aktivitet(AktivitetId) ON DELETE CASCADE 
);

-- Leder der varetager en aktivitet 

CREATE TABLE AktivitetLeder (
    
    BrugerId int NOT NULL,
    AktivitetId int NOT NULL,
    PRIMARY KEY (BrugerId, AktivitetId),
    FOREIGN KEY (BrugerId) REFERENCES Bruger(BrugerId),
    FOREIGN KEY (AktivitetId) REFERENCES Aktivitet(AktivitetId) ON DELETE CASCADE 
);

-- Tegn som en spejder har

CREATE TABLE BrugerFaerdighed(
    
    BrugerId int NOT NULL,
    FaerdighedId int NOT NULL,
    PRIMARY KEY (BrugerId, FaerdighedId),
    FOREIGN KEY (BrugerId) REFERENCES Bruger (BrugerId),
    FOREIGN KEY (FaerdighedId) REFERENCES Faerdighed(FaerdighedId)
);

-- Tegn som aktivitetstype kræver 

CREATE TABLE AktivitetTypeKrav(
    
    AktivitetTypeId int NOT NULL,
    FaerdighedId    int NOT NULL,
    PRIMARY KEY (AktivitetTypeId, FaerdighedId),
    FOREIGN KEY (AktivitetTypeId) REFERENCES AktivitetType(AktivitetTypeId),
    FOREIGN KEY (FaerdighedId) REFERENCES Faerdighed(FaerdighedId)
);