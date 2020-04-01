/*
Navicat MySQL Data Transfer

Source Server         : 121.199.26.254
Source Server Version : 80017
Source Host           : 121.199.26.254:3306
Source Database       : gourd_ids4

Target Server Type    : MYSQL
Target Server Version : 80017
File Encoding         : 65001

Date: 2020-03-13 19:01:22
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
DROP TABLE IF EXISTS `__EFMigrationsHistory`;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of __EFMigrationsHistory
-- ----------------------------
INSERT INTO `__EFMigrationsHistory` VALUES ('20200311043232_AppDbMigration', '3.1.1');
INSERT INTO `__EFMigrationsHistory` VALUES ('20200312085758_InitialIdentityServerPersistedGrantDbMigration', '3.1.1');
INSERT INTO `__EFMigrationsHistory` VALUES ('20200312085822_InitialIdentityServerConfigurationDbMigration', '3.1.1');

-- ----------------------------
-- Table structure for ApiClaims
-- ----------------------------
DROP TABLE IF EXISTS `ApiClaims`;
CREATE TABLE `ApiClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Type` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ApiResourceId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ApiClaims_ApiResourceId` (`ApiResourceId`),
  CONSTRAINT `FK_ApiClaims_ApiResources_ApiResourceId` FOREIGN KEY (`ApiResourceId`) REFERENCES `ApiResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ApiClaims
-- ----------------------------

-- ----------------------------
-- Table structure for ApiProperties
-- ----------------------------
DROP TABLE IF EXISTS `ApiProperties`;
CREATE TABLE `ApiProperties` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Key` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ApiResourceId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ApiProperties_ApiResourceId` (`ApiResourceId`),
  CONSTRAINT `FK_ApiProperties_ApiResources_ApiResourceId` FOREIGN KEY (`ApiResourceId`) REFERENCES `ApiResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ApiProperties
-- ----------------------------

-- ----------------------------
-- Table structure for ApiResources
-- ----------------------------
DROP TABLE IF EXISTS `ApiResources`;
CREATE TABLE `ApiResources` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Enabled` tinyint(1) NOT NULL,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DisplayName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) DEFAULT NULL,
  `LastAccessed` datetime(6) DEFAULT NULL,
  `NonEditable` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_ApiResources_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ApiResources
-- ----------------------------
INSERT INTO `ApiResources` VALUES ('1', '1', 'agentservice', 'CAS Agent Service', null, '2020-03-12 09:16:50.770661', null, null, '0');
INSERT INTO `ApiResources` VALUES ('2', '1', 'productservice', 'CAS Product Service', null, '2020-03-12 09:16:50.770338', null, null, '0');
INSERT INTO `ApiResources` VALUES ('3', '1', 'clientservice', 'CAS Client Service', null, '2020-03-12 09:16:50.731876', null, null, '0');

-- ----------------------------
-- Table structure for ApiScopeClaims
-- ----------------------------
DROP TABLE IF EXISTS `ApiScopeClaims`;
CREATE TABLE `ApiScopeClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Type` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ApiScopeId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ApiScopeClaims_ApiScopeId` (`ApiScopeId`),
  CONSTRAINT `FK_ApiScopeClaims_ApiScopes_ApiScopeId` FOREIGN KEY (`ApiScopeId`) REFERENCES `ApiScopes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ApiScopeClaims
-- ----------------------------

-- ----------------------------
-- Table structure for ApiScopes
-- ----------------------------
DROP TABLE IF EXISTS `ApiScopes`;
CREATE TABLE `ApiScopes` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DisplayName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Required` tinyint(1) NOT NULL,
  `Emphasize` tinyint(1) NOT NULL,
  `ShowInDiscoveryDocument` tinyint(1) NOT NULL,
  `ApiResourceId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_ApiScopes_Name` (`Name`),
  KEY `IX_ApiScopes_ApiResourceId` (`ApiResourceId`),
  CONSTRAINT `FK_ApiScopes_ApiResources_ApiResourceId` FOREIGN KEY (`ApiResourceId`) REFERENCES `ApiResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ApiScopes
-- ----------------------------
INSERT INTO `ApiScopes` VALUES ('1', 'agentservice', 'CAS Agent Service', null, '0', '0', '1', '1');
INSERT INTO `ApiScopes` VALUES ('2', 'productservice', 'CAS Product Service', null, '0', '0', '1', '2');
INSERT INTO `ApiScopes` VALUES ('3', 'clientservice', 'CAS Client Service', null, '0', '0', '1', '3');

-- ----------------------------
-- Table structure for ApiSecrets
-- ----------------------------
DROP TABLE IF EXISTS `ApiSecrets`;
CREATE TABLE `ApiSecrets` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Expiration` datetime(6) DEFAULT NULL,
  `Type` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Created` datetime(6) NOT NULL,
  `ApiResourceId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ApiSecrets_ApiResourceId` (`ApiResourceId`),
  CONSTRAINT `FK_ApiSecrets_ApiResources_ApiResourceId` FOREIGN KEY (`ApiResourceId`) REFERENCES `ApiResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ApiSecrets
-- ----------------------------
INSERT INTO `ApiSecrets` VALUES ('1', null, 'mXeHfZEGxLa8CXZxn5yhFcvBoTL3VFqvJcXvN2scoKw=', null, 'SharedSecret', '2020-03-12 09:16:50.770667', '1');
INSERT INTO `ApiSecrets` VALUES ('2', null, 'xX4Au8Fe7C2CTjf7+sll21ZWx1stroj/SwspKQxgq5Y=', null, 'SharedSecret', '2020-03-12 09:16:50.770667', '1');
INSERT INTO `ApiSecrets` VALUES ('3', null, '3kvQid8Y7WHBICH6/2g6cgkbVy9VUcFIkW24+cxnHFk=', null, 'SharedSecret', '2020-03-12 09:16:50.770666', '1');
INSERT INTO `ApiSecrets` VALUES ('4', null, 'xX4Au8Fe7C2CTjf7+sll21ZWx1stroj/SwspKQxgq5Y=', null, 'SharedSecret', '2020-03-12 09:16:50.770356', '2');
INSERT INTO `ApiSecrets` VALUES ('5', null, '3kvQid8Y7WHBICH6/2g6cgkbVy9VUcFIkW24+cxnHFk=', null, 'SharedSecret', '2020-03-12 09:16:50.770355', '2');
INSERT INTO `ApiSecrets` VALUES ('6', null, '3kvQid8Y7WHBICH6/2g6cgkbVy9VUcFIkW24+cxnHFk=', null, 'SharedSecret', '2020-03-12 09:16:50.737243', '3');

-- ----------------------------
-- Table structure for AspNetRoleClaims
-- ----------------------------
DROP TABLE IF EXISTS `AspNetRoleClaims`;
CREATE TABLE `AspNetRoleClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of AspNetRoleClaims
-- ----------------------------

-- ----------------------------
-- Table structure for AspNetRoles
-- ----------------------------
DROP TABLE IF EXISTS `AspNetRoles`;
CREATE TABLE `AspNetRoles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of AspNetRoles
-- ----------------------------
INSERT INTO `AspNetRoles` VALUES ('08d7c67e-16ad-4b1b-8859-815e76948e9c', '管理员角色', '管理员角色', '367609ee-2cf9-4ef4-964a-2cb894109dba');
INSERT INTO `AspNetRoles` VALUES ('08d7c67e-1ce3-48f7-83be-49a25f0109e7', 'test1', 'TEST1', '25399687-05e2-4325-a6e6-2dc5890b2cdc');
INSERT INTO `AspNetRoles` VALUES ('08d7c67e-1d1d-4ce8-8856-59945707141e', 'test', 'TEST', 'e84fd9e9-68ac-4274-a2b3-08ed7c463d5a');
INSERT INTO `AspNetRoles` VALUES ('08d7c67e-1d54-447c-8641-36d61316da82', '121212', '121212', 'bb61f8b5-f504-45cd-bbbf-4bb339a6498d');

-- ----------------------------
-- Table structure for AspNetUserClaims
-- ----------------------------
DROP TABLE IF EXISTS `AspNetUserClaims`;
CREATE TABLE `AspNetUserClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of AspNetUserClaims
-- ----------------------------
INSERT INTO `AspNetUserClaims` VALUES ('1', '08d7c67c-e528-48f0-83d0-eef65a7fffeb', 'name', 'test');
INSERT INTO `AspNetUserClaims` VALUES ('2', '08d7c67c-e528-48f0-83d0-eef65a7fffeb', 'email', 'test@email.com');
INSERT INTO `AspNetUserClaims` VALUES ('3', '08d7c67c-e528-48f0-83d0-eef65a7fffeb', 'role', 'c4a81038-9c54-4c54-1219-08d74933d1e2');
INSERT INTO `AspNetUserClaims` VALUES ('4', '08d7c67d-113b-4224-8d8e-69f41889cb41', 'role', '2c44f451-dabf-4752-168e-08d6ee366235');
INSERT INTO `AspNetUserClaims` VALUES ('5', '08d7c67d-113b-4224-8d8e-69f41889cb41', 'email', 'uwl@email.com');
INSERT INTO `AspNetUserClaims` VALUES ('6', '08d7c67d-113b-4224-8d8e-69f41889cb41', 'name', 'admin');
INSERT INTO `AspNetUserClaims` VALUES ('7', '08d7c67e-06a1-4084-8a4a-f03240e544bc', 'name', 'admin');
INSERT INTO `AspNetUserClaims` VALUES ('8', '08d7c67e-06a1-4084-8a4a-f03240e544bc', 'email', 'uwl@email.com');
INSERT INTO `AspNetUserClaims` VALUES ('9', '08d7c67e-06a1-4084-8a4a-f03240e544bc', 'role', '2c44f451-dabf-4752-168e-08d6ee366235');

-- ----------------------------
-- Table structure for AspNetUserLogins
-- ----------------------------
DROP TABLE IF EXISTS `AspNetUserLogins`;
CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of AspNetUserLogins
-- ----------------------------

-- ----------------------------
-- Table structure for AspNetUserRoles
-- ----------------------------
DROP TABLE IF EXISTS `AspNetUserRoles`;
CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of AspNetUserRoles
-- ----------------------------

-- ----------------------------
-- Table structure for AspNetUsers
-- ----------------------------
DROP TABLE IF EXISTS `AspNetUsers`;
CREATE TABLE `AspNetUsers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of AspNetUsers
-- ----------------------------
INSERT INTO `AspNetUsers` VALUES ('08d7c67c-e528-48f0-83d0-eef65a7fffeb', 'test', 'TEST', null, null, '0', 'AQAAAAEAACcQAAAAEBikGiQmRvlw3b+z29vZs/3X5WppQUIzLUN4T/3VkM/ASHeZP7kEj5LbZfh5j4T5rA==', 'X6QLQCRDRSTWHBK6MAHWUWIKY2MUCBKO', 'b1260b8f-edc8-44b3-bfcc-9e35718ef3cf', null, '0', '0', null, '1', '0');
INSERT INTO `AspNetUsers` VALUES ('08d7c67d-113b-4224-8d8e-69f41889cb41', 'uwl', 'UWL', null, null, '0', 'AQAAAAEAACcQAAAAEOzX7q1zyEDwAl4U/hLkI7zg2Y4mPrD0r3VHkrXvHxLkWWS4xE/JER4JhSDw5YRpCw==', 'IJE6EP525MMQMJJQWDCU75KTCQK3HTBE', '865c5bb7-504f-4a16-ba0c-0ae1d3796076', null, '0', '0', null, '1', '0');
INSERT INTO `AspNetUsers` VALUES ('08d7c67e-06a1-4084-8a4a-f03240e544bc', 'admin', 'ADMIN', null, null, '0', 'AQAAAAEAACcQAAAAEMn5hUyamt8/9wobiW20RKndNTEQyiE9cINjo1zWAt9nv/YLN8Xj13neMlweM/J82w==', 'J7EKZ7HGPQAWDXTIJM7TDRQHJKREACSY', '05bbfafe-08d0-46e3-8316-5c8c1f3d4181', null, '0', '0', null, '1', '0');

-- ----------------------------
-- Table structure for AspNetUserTokens
-- ----------------------------
DROP TABLE IF EXISTS `AspNetUserTokens`;
CREATE TABLE `AspNetUserTokens` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of AspNetUserTokens
-- ----------------------------

-- ----------------------------
-- Table structure for ClientClaims
-- ----------------------------
DROP TABLE IF EXISTS `ClientClaims`;
CREATE TABLE `ClientClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Type` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientClaims_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientClaims_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ClientClaims
-- ----------------------------

-- ----------------------------
-- Table structure for ClientCorsOrigins
-- ----------------------------
DROP TABLE IF EXISTS `ClientCorsOrigins`;
CREATE TABLE `ClientCorsOrigins` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Origin` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientCorsOrigins_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientCorsOrigins_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ClientCorsOrigins
-- ----------------------------
INSERT INTO `ClientCorsOrigins` VALUES ('1', 'http://localhost:6011', '5');
INSERT INTO `ClientCorsOrigins` VALUES ('2', 'http://localhost:6012', '5');
INSERT INTO `ClientCorsOrigins` VALUES ('3', 'http://localhost:6013', '5');

-- ----------------------------
-- Table structure for ClientGrantTypes
-- ----------------------------
DROP TABLE IF EXISTS `ClientGrantTypes`;
CREATE TABLE `ClientGrantTypes` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GrantType` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientGrantTypes_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientGrantTypes_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ClientGrantTypes
-- ----------------------------
INSERT INTO `ClientGrantTypes` VALUES ('1', 'password', '1');
INSERT INTO `ClientGrantTypes` VALUES ('2', 'implicit', '4');
INSERT INTO `ClientGrantTypes` VALUES ('3', 'password', '3');
INSERT INTO `ClientGrantTypes` VALUES ('4', 'implicit', '5');
INSERT INTO `ClientGrantTypes` VALUES ('5', 'client_credentials', '3');
INSERT INTO `ClientGrantTypes` VALUES ('6', 'client_credentials', '2');
INSERT INTO `ClientGrantTypes` VALUES ('7', 'password', '2');
INSERT INTO `ClientGrantTypes` VALUES ('8', 'client_credentials', '1');

-- ----------------------------
-- Table structure for ClientIdPRestrictions
-- ----------------------------
DROP TABLE IF EXISTS `ClientIdPRestrictions`;
CREATE TABLE `ClientIdPRestrictions` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Provider` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientIdPRestrictions_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientIdPRestrictions_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ClientIdPRestrictions
-- ----------------------------

-- ----------------------------
-- Table structure for ClientPostLogoutRedirectUris
-- ----------------------------
DROP TABLE IF EXISTS `ClientPostLogoutRedirectUris`;
CREATE TABLE `ClientPostLogoutRedirectUris` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PostLogoutRedirectUri` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientPostLogoutRedirectUris_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientPostLogoutRedirectUris_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ClientPostLogoutRedirectUris
-- ----------------------------
INSERT INTO `ClientPostLogoutRedirectUris` VALUES ('1', 'http://localhost:6012/js/index.html', '5');
INSERT INTO `ClientPostLogoutRedirectUris` VALUES ('2', 'http://localhost:6012/signin-oidc/signout-callback-oidc', '4');
INSERT INTO `ClientPostLogoutRedirectUris` VALUES ('3', 'http://localhost:6013/js/index.html', '5');
INSERT INTO `ClientPostLogoutRedirectUris` VALUES ('4', 'http://localhost:6013/signin-oidc/signout-callback-oidc', '4');
INSERT INTO `ClientPostLogoutRedirectUris` VALUES ('5', 'http://localhost:6011/js/index.html', '5');
INSERT INTO `ClientPostLogoutRedirectUris` VALUES ('6', 'http://localhost:6011/signin-oidc/signout-callback-oidc', '4');

-- ----------------------------
-- Table structure for ClientProperties
-- ----------------------------
DROP TABLE IF EXISTS `ClientProperties`;
CREATE TABLE `ClientProperties` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Key` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientProperties_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientProperties_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ClientProperties
-- ----------------------------

-- ----------------------------
-- Table structure for ClientRedirectUris
-- ----------------------------
DROP TABLE IF EXISTS `ClientRedirectUris`;
CREATE TABLE `ClientRedirectUris` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RedirectUri` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientRedirectUris_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientRedirectUris_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ClientRedirectUris
-- ----------------------------
INSERT INTO `ClientRedirectUris` VALUES ('1', 'http://localhost:6011/js/callback.html', '5');
INSERT INTO `ClientRedirectUris` VALUES ('2', 'http://localhost:6013/signin-oidc', '4');
INSERT INTO `ClientRedirectUris` VALUES ('3', 'http://localhost:6012/signin-oidc', '4');
INSERT INTO `ClientRedirectUris` VALUES ('4', 'http://localhost:6011/signin-oidc', '4');
INSERT INTO `ClientRedirectUris` VALUES ('5', 'http://localhost:6013/js/callback.html', '5');
INSERT INTO `ClientRedirectUris` VALUES ('6', 'http://localhost:6012/js/callback.html', '5');

-- ----------------------------
-- Table structure for Clients
-- ----------------------------
DROP TABLE IF EXISTS `Clients`;
CREATE TABLE `Clients` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Enabled` tinyint(1) NOT NULL,
  `ClientId` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProtocolType` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RequireClientSecret` tinyint(1) NOT NULL,
  `ClientName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ClientUri` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `LogoUri` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `RequireConsent` tinyint(1) NOT NULL,
  `AllowRememberConsent` tinyint(1) NOT NULL,
  `AlwaysIncludeUserClaimsInIdToken` tinyint(1) NOT NULL,
  `RequirePkce` tinyint(1) NOT NULL,
  `AllowPlainTextPkce` tinyint(1) NOT NULL,
  `AllowAccessTokensViaBrowser` tinyint(1) NOT NULL,
  `FrontChannelLogoutUri` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `FrontChannelLogoutSessionRequired` tinyint(1) NOT NULL,
  `BackChannelLogoutUri` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `BackChannelLogoutSessionRequired` tinyint(1) NOT NULL,
  `AllowOfflineAccess` tinyint(1) NOT NULL,
  `IdentityTokenLifetime` int(11) NOT NULL,
  `AccessTokenLifetime` int(11) NOT NULL,
  `AuthorizationCodeLifetime` int(11) NOT NULL,
  `ConsentLifetime` int(11) DEFAULT NULL,
  `AbsoluteRefreshTokenLifetime` int(11) NOT NULL,
  `SlidingRefreshTokenLifetime` int(11) NOT NULL,
  `RefreshTokenUsage` int(11) NOT NULL,
  `UpdateAccessTokenClaimsOnRefresh` tinyint(1) NOT NULL,
  `RefreshTokenExpiration` int(11) NOT NULL,
  `AccessTokenType` int(11) NOT NULL,
  `EnableLocalLogin` tinyint(1) NOT NULL,
  `IncludeJwtId` tinyint(1) NOT NULL,
  `AlwaysSendClientClaims` tinyint(1) NOT NULL,
  `ClientClaimsPrefix` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `PairWiseSubjectSalt` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) DEFAULT NULL,
  `LastAccessed` datetime(6) DEFAULT NULL,
  `UserSsoLifetime` int(11) DEFAULT NULL,
  `UserCodeType` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `DeviceCodeLifetime` int(11) NOT NULL,
  `NonEditable` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Clients_ClientId` (`ClientId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Clients
-- ----------------------------
INSERT INTO `Clients` VALUES ('1', '1', 'client.api.service', 'oidc', '1', null, null, null, null, '1', '1', '0', '0', '0', '0', null, '1', null, '1', '0', '300', '3600', '300', null, '2592000', '1296000', '1', '0', '1', '0', '1', '0', '0', 'client_', null, '2020-03-12 09:16:48.319752', null, null, null, null, '300', '0');
INSERT INTO `Clients` VALUES ('2', '1', 'product.api.service', 'oidc', '1', null, null, null, null, '1', '1', '0', '0', '0', '0', null, '1', null, '1', '0', '300', '3600', '300', null, '2592000', '1296000', '1', '0', '1', '0', '1', '0', '0', 'client_', null, '2020-03-12 09:16:48.482977', null, null, null, null, '300', '0');
INSERT INTO `Clients` VALUES ('3', '1', 'agent.api.service', 'oidc', '1', null, null, null, null, '1', '1', '0', '0', '0', '0', null, '1', null, '1', '0', '300', '3600', '300', null, '2592000', '1296000', '1', '0', '1', '0', '1', '0', '0', 'client_', null, '2020-03-12 09:16:48.486812', null, null, null, null, '300', '0');
INSERT INTO `Clients` VALUES ('4', '1', 'cas.mvc.client.implicit', 'oidc', '1', 'CAS MVC Web App Client', null, null, null, '1', '1', '0', '0', '0', '1', null, '1', null, '1', '0', '300', '3600', '300', null, '2592000', '1296000', '1', '0', '1', '0', '1', '0', '0', 'client_', null, '2020-03-12 09:16:48.487274', null, null, null, null, '300', '0');
INSERT INTO `Clients` VALUES ('5', '1', 'js', 'oidc', '1', 'JavaScript Client', null, null, null, '1', '1', '0', '0', '0', '1', null, '1', null, '1', '0', '300', '3600', '300', null, '2592000', '1296000', '1', '0', '1', '0', '1', '0', '0', 'client_', null, '2020-03-12 09:16:48.504845', null, null, null, null, '300', '0');

-- ----------------------------
-- Table structure for ClientScopes
-- ----------------------------
DROP TABLE IF EXISTS `ClientScopes`;
CREATE TABLE `ClientScopes` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Scope` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientScopes_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientScopes_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ClientScopes
-- ----------------------------
INSERT INTO `ClientScopes` VALUES ('1', 'clientservice', '1');
INSERT INTO `ClientScopes` VALUES ('2', 'clientservice', '2');
INSERT INTO `ClientScopes` VALUES ('3', 'productservice', '2');
INSERT INTO `ClientScopes` VALUES ('4', 'agentservice', '5');
INSERT INTO `ClientScopes` VALUES ('5', 'profile', '5');
INSERT INTO `ClientScopes` VALUES ('6', 'openid', '5');
INSERT INTO `ClientScopes` VALUES ('7', 'productservice', '4');
INSERT INTO `ClientScopes` VALUES ('8', 'clientservice', '4');
INSERT INTO `ClientScopes` VALUES ('9', 'clientservice', '3');
INSERT INTO `ClientScopes` VALUES ('10', 'productservice', '3');
INSERT INTO `ClientScopes` VALUES ('11', 'openid', '4');
INSERT INTO `ClientScopes` VALUES ('12', 'profile', '4');
INSERT INTO `ClientScopes` VALUES ('13', 'agentservice', '4');
INSERT INTO `ClientScopes` VALUES ('14', 'agentservice', '3');

-- ----------------------------
-- Table structure for ClientSecrets
-- ----------------------------
DROP TABLE IF EXISTS `ClientSecrets`;
CREATE TABLE `ClientSecrets` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Expiration` datetime(6) DEFAULT NULL,
  `Type` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Created` datetime(6) NOT NULL,
  `ClientId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ClientSecrets_ClientId` (`ClientId`),
  CONSTRAINT `FK_ClientSecrets_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ClientSecrets
-- ----------------------------
INSERT INTO `ClientSecrets` VALUES ('1', null, 'G9dz8ScihnLhwvropw7S3eukDzKPMp6hLMB8CC7GMb4=', null, 'SharedSecret', '2020-03-12 09:16:48.486816', '3');
INSERT INTO `ClientSecrets` VALUES ('2', null, 'w7Johiu2r4I3AhRKUrOaHDHdVEG5aNa179uSXW719m0=', null, 'SharedSecret', '2020-03-12 09:16:48.320385', '1');
INSERT INTO `ClientSecrets` VALUES ('3', null, 'gfDm/p/e8H551braYU9Nhim6oKtAXpz+6/KBi1bGO18=', null, 'SharedSecret', '2020-03-12 09:16:48.482995', '2');

-- ----------------------------
-- Table structure for IdentityClaims
-- ----------------------------
DROP TABLE IF EXISTS `IdentityClaims`;
CREATE TABLE `IdentityClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Type` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IdentityResourceId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_IdentityClaims_IdentityResourceId` (`IdentityResourceId`),
  CONSTRAINT `FK_IdentityClaims_IdentityResources_IdentityResourceId` FOREIGN KEY (`IdentityResourceId`) REFERENCES `IdentityResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of IdentityClaims
-- ----------------------------
INSERT INTO `IdentityClaims` VALUES ('1', 'updated_at', '1');
INSERT INTO `IdentityClaims` VALUES ('2', 'locale', '1');
INSERT INTO `IdentityClaims` VALUES ('3', 'zoneinfo', '1');
INSERT INTO `IdentityClaims` VALUES ('4', 'birthdate', '1');
INSERT INTO `IdentityClaims` VALUES ('5', 'gender', '1');
INSERT INTO `IdentityClaims` VALUES ('6', 'website', '1');
INSERT INTO `IdentityClaims` VALUES ('7', 'picture', '1');
INSERT INTO `IdentityClaims` VALUES ('8', 'profile', '1');
INSERT INTO `IdentityClaims` VALUES ('9', 'preferred_username', '1');
INSERT INTO `IdentityClaims` VALUES ('10', 'nickname', '1');
INSERT INTO `IdentityClaims` VALUES ('11', 'middle_name', '1');
INSERT INTO `IdentityClaims` VALUES ('12', 'given_name', '1');
INSERT INTO `IdentityClaims` VALUES ('13', 'family_name', '1');
INSERT INTO `IdentityClaims` VALUES ('14', 'name', '1');
INSERT INTO `IdentityClaims` VALUES ('15', 'sub', '2');

-- ----------------------------
-- Table structure for IdentityProperties
-- ----------------------------
DROP TABLE IF EXISTS `IdentityProperties`;
CREATE TABLE `IdentityProperties` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Key` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IdentityResourceId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_IdentityProperties_IdentityResourceId` (`IdentityResourceId`),
  CONSTRAINT `FK_IdentityProperties_IdentityResources_IdentityResourceId` FOREIGN KEY (`IdentityResourceId`) REFERENCES `IdentityResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of IdentityProperties
-- ----------------------------

-- ----------------------------
-- Table structure for IdentityResources
-- ----------------------------
DROP TABLE IF EXISTS `IdentityResources`;
CREATE TABLE `IdentityResources` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Enabled` tinyint(1) NOT NULL,
  `Name` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DisplayName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Required` tinyint(1) NOT NULL,
  `Emphasize` tinyint(1) NOT NULL,
  `ShowInDiscoveryDocument` tinyint(1) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Updated` datetime(6) DEFAULT NULL,
  `NonEditable` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_IdentityResources_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of IdentityResources
-- ----------------------------
INSERT INTO `IdentityResources` VALUES ('1', '1', 'profile', 'User profile', 'Your user profile information (first name, last name, etc.)', '0', '1', '1', '2020-03-12 09:16:50.080079', null, '0');
INSERT INTO `IdentityResources` VALUES ('2', '1', 'openid', 'Your user identifier', null, '1', '0', '1', '2020-03-12 09:16:50.057873', null, '0');
