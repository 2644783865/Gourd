/*
Navicat MySQL Data Transfer

Source Server         : 121.199.26.254
Source Server Version : 80017
Source Host           : 121.199.26.254:3306
Source Database       : gourd

Target Server Type    : MYSQL
Target Server Version : 80017
File Encoding         : 65001

Date: 2020-03-08 15:59:00
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for UserAccount
-- ----------------------------
DROP TABLE IF EXISTS `UserAccount`;
CREATE TABLE `UserAccount` (
  `Id` varchar(50) NOT NULL,
  `UserID` varchar(50) DEFAULT NULL,
  `Integral` decimal(18,0) DEFAULT NULL,
  `Assets` decimal(18,0) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for UserInfo
-- ----------------------------
DROP TABLE IF EXISTS `UserInfo`;
CREATE TABLE `UserInfo` (
  `Id` varchar(50) DEFAULT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Account` varchar(50) DEFAULT NULL,
  `Pwd` varchar(50) DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `Status` int(11) DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for UserLogin
-- ----------------------------
DROP TABLE IF EXISTS `UserLogin`;
CREATE TABLE `UserLogin` (
  `Id` varchar(50) NOT NULL,
  `LoginTime` datetime DEFAULT NULL,
  `LoginIP` varchar(50) DEFAULT NULL,
  `LoginType` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
