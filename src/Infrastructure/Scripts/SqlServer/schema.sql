-- Creación de la base de datos
DROP DATABASE IF EXISTS AutoTaller;
CREATE DATABASE AutoTaller
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE AutoTaller;

-- Tabla: Roles
CREATE TABLE Roles (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(50) NOT NULL,
    Description VARCHAR(200),
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: Users
CREATE TABLE Users (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100),
    UNIQUE KEY UK_Users_Email (Email)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: UserRoles
CREATE TABLE UserRoles (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100),
    CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_UserRoles_Roles FOREIGN KEY (RoleId) REFERENCES Roles(Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: Audits
CREATE TABLE Audits (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    TableName VARCHAR(50) NOT NULL,
    ActionType VARCHAR(20) NOT NULL,
    UserId INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100),
    CONSTRAINT FK_Audits_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: Specializations
CREATE TABLE Specializations (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(50) NOT NULL,
    Description VARCHAR(200),
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: UserSpecializations
CREATE TABLE UserSpecializations (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    UserId INT NOT NULL,
    SpecializationId INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100),
    CONSTRAINT FK_UserSpecializations_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_UserSpecializations_Specializations FOREIGN KEY (SpecializationId) REFERENCES Specializations(Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: Status
CREATE TABLE Status (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(25) NOT NULL,
    Description VARCHAR(200),
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: Customers
CREATE TABLE Customers (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Phone VARCHAR(20),
    Email VARCHAR(100),
    IdentificationNumber VARCHAR(30),
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: Vehicles
CREATE TABLE Vehicles (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    CustomerId INT NOT NULL,
    Brand VARCHAR(50) NOT NULL,
    Model VARCHAR(50) NOT NULL,
    Year INT NOT NULL,
    VIN VARCHAR(100) NOT NULL,
    Mileage INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100),
    UNIQUE KEY UK_Vehicles_VIN (VIN),
    CONSTRAINT FK_Vehicles_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: ServiceTypes
CREATE TABLE ServiceTypes (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(50) NOT NULL,
    Description VARCHAR(200),
    Duration INT NOT NULL,
    BasePrice DECIMAL(18,2) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: ServiceOrders
CREATE TABLE ServiceOrders (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    VehicleId INT NOT NULL,
    MechanicId INT NOT NULL,
    ServiceTypeId INT NOT NULL,
    StatusId INT NOT NULL,
    EntryDate DATETIME NOT NULL,
    ExitDate DATETIME NULL,
    CustomerMessage VARCHAR(500),
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100),
    CONSTRAINT FK_ServiceOrders_Vehicles FOREIGN KEY (VehicleId) REFERENCES Vehicles(Id),
    CONSTRAINT FK_ServiceOrders_Users FOREIGN KEY (MechanicId) REFERENCES Users(Id),
    CONSTRAINT FK_ServiceOrders_ServiceTypes FOREIGN KEY (ServiceTypeId) REFERENCES ServiceTypes(Id),
    CONSTRAINT FK_ServiceOrders_Status FOREIGN KEY (StatusId) REFERENCES Status(Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: Diagnostics
CREATE TABLE Diagnostics (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    ServiceOrderId INT NOT NULL,
    Description VARCHAR(500) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100),
    CONSTRAINT FK_Diagnostics_ServiceOrders FOREIGN KEY (ServiceOrderId) REFERENCES ServiceOrders(Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: DiagnosticDetails
CREATE TABLE DiagnosticDetails (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    DiagnosticId INT NOT NULL,
    Description VARCHAR(500) NOT NULL,
    Observation VARCHAR(500),
    EstimatedCost DECIMAL(18,2) NOT NULL,
    Priority INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100),
    CONSTRAINT FK_DiagnosticDetails_Diagnostics FOREIGN KEY (DiagnosticId) REFERENCES Diagnostics(Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: Spares
CREATE TABLE Spares (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Description VARCHAR(500),
    Brand VARCHAR(50) NOT NULL,
    Model VARCHAR(50) NOT NULL,
    SerialNumber VARCHAR(50),
    UnitPrice DECIMAL(18,2) NOT NULL,
    StockQuantity INT NOT NULL,
    MinimumStock INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: OrderDetails
CREATE TABLE OrderDetails (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    ServiceOrderId INT NOT NULL,
    SpareId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    Discount DECIMAL(18,2),
    Total DECIMAL(18,2) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100),
    CONSTRAINT FK_OrderDetails_ServiceOrders FOREIGN KEY (ServiceOrderId) REFERENCES ServiceOrders(Id),
    CONSTRAINT FK_OrderDetails_Spares FOREIGN KEY (SpareId) REFERENCES Spares(Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: Invoices
CREATE TABLE Invoices (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    ServiceOrderId INT NOT NULL,
    Number VARCHAR(20) NOT NULL,
    Date DATETIME NOT NULL,
    Subtotal DECIMAL(18,2) NOT NULL,
    TaxRate DECIMAL(5,2) NOT NULL,
    TaxAmount DECIMAL(18,2) NOT NULL,
    DiscountRate DECIMAL(5,2) NOT NULL,
    DiscountAmount DECIMAL(18,2) NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    PaymentMethod VARCHAR(50) NOT NULL,
    PaymentStatus VARCHAR(50) NOT NULL,
    Notes VARCHAR(500),
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100),
    UNIQUE KEY UK_Invoices_Number (Number),
    CONSTRAINT FK_Invoices_ServiceOrders FOREIGN KEY (ServiceOrderId) REFERENCES ServiceOrders(Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: Inventory
CREATE TABLE Inventory (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Date DATETIME NOT NULL,
    Type VARCHAR(20) NOT NULL,
    Notes VARCHAR(500),
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla: InventoryDetails
CREATE TABLE InventoryDetails (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    InventoryId INT NOT NULL,
    SpareId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitCost DECIMAL(18,2) NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    BatchNumber VARCHAR(50) NOT NULL,
    ExpirationDate DATETIME NULL,
    Location VARCHAR(50) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT UTC_TIMESTAMP(),
    CreatedBy VARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UpdatedBy VARCHAR(100),
    IsDeleted BOOLEAN NOT NULL DEFAULT FALSE,
    DeletedAt DATETIME NULL,
    DeletedBy VARCHAR(100),
    CONSTRAINT FK_InventoryDetails_Inventory FOREIGN KEY (InventoryId) REFERENCES Inventory(Id),
    CONSTRAINT FK_InventoryDetails_Spares FOREIGN KEY (SpareId) REFERENCES Spares(Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci; 