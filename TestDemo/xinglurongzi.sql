/*
Navicat MySQL Data Transfer

Source Server         : 本地
Source Server Version : 50640
Source Host           : 127.0.0.1:3306
Source Database       : xinglurongzi

Target Server Type    : MYSQL
Target Server Version : 50640
File Encoding         : 65001

Date: 2018-10-14 22:23:01
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for menueditor_info
-- ----------------------------
DROP TABLE IF EXISTS `menueditor_info`;
CREATE TABLE `menueditor_info` (
  `idstr` int(11) NOT NULL AUTO_INCREMENT,
  `canshutype` varchar(100) DEFAULT NULL,
  `canshulist` varchar(1024) DEFAULT NULL,
  `updatetime` datetime DEFAULT NULL,
  `createtime` datetime DEFAULT NULL,
  `remark` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`idstr`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of menueditor_info
-- ----------------------------
INSERT INTO `menueditor_info` VALUES ('1', '债务人', 'aa;bb;cc;dd;ee;ff;gg;', '2018-10-13 10:39:08', '2018-10-13 00:00:00', null);
INSERT INTO `menueditor_info` VALUES ('2', '债券人', '1;2;3;4;5;6;7;', '2018-10-13 10:38:51', '2018-10-13 00:00:00', null);
INSERT INTO `menueditor_info` VALUES ('3', '融资方式', 'A;B;C;D;E;F;G;', '2018-10-13 10:38:51', '2018-10-13 00:00:00', null);

-- ----------------------------
-- Table structure for projectzijin_userinfo
-- ----------------------------
DROP TABLE IF EXISTS `projectzijin_userinfo`;
CREATE TABLE `projectzijin_userinfo` (
  `idstr` int(20) NOT NULL AUTO_INCREMENT,
  `lururen` varchar(100) DEFAULT NULL,
  `lurudate` date DEFAULT NULL,
  `projectname` varchar(100) DEFAULT NULL,
  `zhaiwuname` varchar(100) DEFAULT NULL,
  `zhaiwujine` decimal(32,2) DEFAULT NULL,
  `zhifuxingzhi` varchar(40) DEFAULT NULL,
  `zhifudate` date DEFAULT NULL,
  `zhifujine` decimal(32,2) DEFAULT NULL,
  `shouhuidate` date DEFAULT NULL,
  `shouhuijine` decimal(32,2) DEFAULT NULL,
  `remark` varchar(100) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL,
  PRIMARY KEY (`idstr`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of projectzijin_userinfo
-- ----------------------------

-- ----------------------------
-- Table structure for project_info
-- ----------------------------
DROP TABLE IF EXISTS `project_info`;
CREATE TABLE `project_info` (
  `idstr` int(20) NOT NULL AUTO_INCREMENT,
  `projectname` varchar(100) DEFAULT NULL,
  `projectdanwei` varchar(100) DEFAULT NULL,
  `projectzongtou` decimal(32,2) DEFAULT NULL,
  `projectstartdate` date DEFAULT NULL,
  `projectcompletedate` date DEFAULT NULL,
  `prpjecttype` varchar(40) DEFAULT NULL,
  `projectstatus` varchar(40) DEFAULT NULL,
  `updatetime` datetime DEFAULT NULL,
  `createtime` datetime DEFAULT NULL,
  `remark` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`idstr`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of project_info
-- ----------------------------

-- ----------------------------
-- Table structure for rongzianpai_info
-- ----------------------------
DROP TABLE IF EXISTS `rongzianpai_info`;
CREATE TABLE `rongzianpai_info` (
  `idstr` int(20) NOT NULL AUTO_INCREMENT,
  `luruname` varchar(100) DEFAULT NULL,
  `lurudate` date DEFAULT NULL,
  `zhaiwuname` varchar(100) DEFAULT NULL,
  `projectname` varchar(100) DEFAULT NULL,
  `anpaijine1` decimal(32,2) DEFAULT NULL,
  `anpaijine2` decimal(32,2) DEFAULT NULL,
  `anpaijine3` decimal(32,2) DEFAULT NULL,
  `anpaidate` date DEFAULT NULL,
  `yuedate` date DEFAULT NULL,
  `remark` varchar(100) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL,
  PRIMARY KEY (`idstr`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of rongzianpai_info
-- ----------------------------

-- ----------------------------
-- Table structure for rongzihuanbenfuxi_info
-- ----------------------------
DROP TABLE IF EXISTS `rongzihuanbenfuxi_info`;
CREATE TABLE `rongzihuanbenfuxi_info` (
  `idstr` int(20) NOT NULL AUTO_INCREMENT,
  `fuxiyear` int(11) DEFAULT NULL,
  `fuxidatetype` varchar(100) DEFAULT NULL,
  `fuxidate` date DEFAULT NULL,
  `jixitianshu` int(11) DEFAULT NULL,
  `nianlilv` double(4,2) DEFAULT NULL,
  `feilv` double(4,2) DEFAULT NULL,
  `daozhangjine` decimal(32,2) DEFAULT NULL,
  `huanbenjine` decimal(32,2) DEFAULT NULL,
  `benjinyue` decimal(32,2) DEFAULT NULL,
  `yingjilixi` decimal(32,2) DEFAULT NULL,
  `yingfufeiyong` decimal(32,2) DEFAULT NULL,
  `shifouqingchang` int(11) DEFAULT NULL,
  `remark` varchar(200) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL,
  PRIMARY KEY (`idstr`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of rongzihuanbenfuxi_info
-- ----------------------------

-- ----------------------------
-- Table structure for rongzitiaozheng_info
-- ----------------------------
DROP TABLE IF EXISTS `rongzitiaozheng_info`;
CREATE TABLE `rongzitiaozheng_info` (
  `idstr` int(20) NOT NULL AUTO_INCREMENT,
  `luruname` varchar(100) DEFAULT NULL,
  `lurudate` date DEFAULT NULL,
  `zijinlaiyuan` varchar(100) DEFAULT NULL,
  `projectnamebefore` varchar(100) DEFAULT NULL,
  `projectjinebefore` decimal(32,2) DEFAULT NULL,
  `projectnamenow` varchar(100) DEFAULT NULL,
  `projectjinenow` decimal(32,2) DEFAULT NULL,
  `remark` varchar(200) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL,
  PRIMARY KEY (`idstr`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of rongzitiaozheng_info
-- ----------------------------

-- ----------------------------
-- Table structure for rongzizhihuan_info
-- ----------------------------
DROP TABLE IF EXISTS `rongzizhihuan_info`;
CREATE TABLE `rongzizhihuan_info` (
  `idstr` int(20) NOT NULL AUTO_INCREMENT,
  `luruname` varchar(100) DEFAULT NULL,
  `lurutime` time DEFAULT NULL,
  `zhihuanjine` decimal(32,2) DEFAULT NULL,
  `zhihuantime` time DEFAULT NULL,
  `zhihuannamebefore` varchar(100) DEFAULT NULL,
  `zhihuannamenow` varchar(100) DEFAULT NULL,
  `remark` varchar(200) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL,
  PRIMARY KEY (`idstr`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of rongzizhihuan_info
-- ----------------------------

-- ----------------------------
-- Table structure for rongzi_info
-- ----------------------------
DROP TABLE IF EXISTS `rongzi_info`;
CREATE TABLE `rongzi_info` (
  `idstr` int(20) NOT NULL AUTO_INCREMENT,
  `zhaiwunrename` varchar(100) DEFAULT NULL,
  `zhaiquanrenname` varchar(100) DEFAULT NULL,
  `zhaiquanrentype` varchar(40) DEFAULT NULL,
  `rongzifangshi` varchar(40) DEFAULT NULL,
  `zhaiwutype` varchar(40) DEFAULT NULL,
  `zhaiwuname` varchar(100) DEFAULT NULL,
  `zijinyongtu` varchar(100) DEFAULT NULL,
  `hetongjine` decimal(32,2) DEFAULT NULL,
  `daoweijineshidian` datetime DEFAULT NULL,
  `daoweijine` decimal(32,2) DEFAULT NULL,
  `rongziyueshidian` datetime DEFAULT NULL,
  `rongziyue` decimal(32,2) DEFAULT NULL,
  `danbaofangshi` varchar(40) DEFAULT NULL,
  `lilvtype` varchar(40) DEFAULT NULL,
  `lilvmiaoshu` varchar(40) DEFAULT NULL,
  `lilvbaifenbi` double(4,2) DEFAULT NULL,
  `feilv` double(4,2) DEFAULT NULL,
  `qixian` int(11) DEFAULT NULL,
  `qixiri` date DEFAULT NULL,
  `daoqiri` date DEFAULT NULL,
  `remark` varchar(200) DEFAULT NULL,
  `hetongno` varchar(100) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL,
  `updatetime` datetime DEFAULT NULL,
  PRIMARY KEY (`idstr`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of rongzi_info
-- ----------------------------

-- ----------------------------
-- Table structure for userlog_info
-- ----------------------------
DROP TABLE IF EXISTS `userlog_info`;
CREATE TABLE `userlog_info` (
  `idstr` int(20) NOT NULL AUTO_INCREMENT,
  `loginname` varchar(100) DEFAULT NULL,
  `logtype` int(11) DEFAULT NULL,
  `logcontent` varchar(512) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL,
  PRIMARY KEY (`idstr`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of userlog_info
-- ----------------------------
INSERT INTO `userlog_info` VALUES ('1', 'ouyang', '1', '第1条日志', '2018-10-13 13:57:30');
INSERT INTO `userlog_info` VALUES ('2', 'zhuge', '1', '第2条日志', '2018-10-13 13:57:31');
INSERT INTO `userlog_info` VALUES ('3', 'shangguan', '1', '第3条日志', '2018-10-13 13:57:31');
INSERT INTO `userlog_info` VALUES ('4', 'sima', '1', '第4条日志', '2018-10-13 13:57:31');

-- ----------------------------
-- Table structure for user_info
-- ----------------------------
DROP TABLE IF EXISTS `user_info`;
CREATE TABLE `user_info` (
  `idstr` int(20) NOT NULL AUTO_INCREMENT,
  `roletype` int(11) DEFAULT NULL,
  `username` varchar(100) DEFAULT NULL,
  `loginname` varchar(100) DEFAULT NULL,
  `loginpass` varchar(100) DEFAULT NULL,
  `rolequanxian` varchar(32) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL,
  `updatetime` datetime DEFAULT NULL,
  `remark` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`idstr`)
) ENGINE=InnoDB AUTO_INCREMENT=539 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of user_info
-- ----------------------------
INSERT INTO `user_info` VALUES ('514', null, '欧阳1', 'ouyang', '123', null, null, null, null);
INSERT INTO `user_info` VALUES ('515', null, '欧阳2', 'ouyang', '123', null, null, null, null);
INSERT INTO `user_info` VALUES ('516', null, '欧阳3', 'ouyang', '123', null, null, null, null);
INSERT INTO `user_info` VALUES ('517', null, '欧阳4', 'ouyang', '123', null, null, null, null);
INSERT INTO `user_info` VALUES ('518', null, '欧阳5', 'ouyang', '123', null, null, null, null);
INSERT INTO `user_info` VALUES ('519', null, '欧阳6', 'ouyang', '123', null, null, null, null);
INSERT INTO `user_info` VALUES ('520', null, '欧阳7', 'ouyang', '123', null, null, null, null);
INSERT INTO `user_info` VALUES ('521', null, '欧阳8', 'ouyang', '123', null, null, null, null);
INSERT INTO `user_info` VALUES ('522', null, '欧阳9', 'ouyang', '123', null, null, null, null);
INSERT INTO `user_info` VALUES ('523', null, '宇峰0', 'ouyang0', '123456', null, null, null, null);
INSERT INTO `user_info` VALUES ('524', null, '宇峰1', 'ouyang1', '123456', null, null, null, null);
INSERT INTO `user_info` VALUES ('525', null, '宇峰2', 'ouyang2', '123456', null, null, null, null);
INSERT INTO `user_info` VALUES ('526', null, '宇峰3', 'ouyang3', '123456', null, null, null, null);
INSERT INTO `user_info` VALUES ('527', null, '宇峰4', 'ouyang4', '123456', null, null, null, null);
INSERT INTO `user_info` VALUES ('528', null, '宇峰5', 'ouyang5', '123456', null, null, null, null);
INSERT INTO `user_info` VALUES ('529', null, '宇峰6', 'ouyang6', '123456', null, null, null, null);
INSERT INTO `user_info` VALUES ('530', null, '宇峰7', 'ouyang7', '123456', null, null, null, null);
INSERT INTO `user_info` VALUES ('531', null, '宇峰8', 'ouyang8', '123456', null, null, null, null);
INSERT INTO `user_info` VALUES ('532', null, '宇峰9', 'ouyang9', '123456', null, null, null, null);
INSERT INTO `user_info` VALUES ('533', null, '宇0', 'ou0', '1', null, null, null, null);
INSERT INTO `user_info` VALUES ('534', null, '宇峰欧阳1', 'ouyangyufeng1', '12', null, null, null, null);
INSERT INTO `user_info` VALUES ('535', '1', '上官', 'shangguan', '123456', '1001', '2018-10-13 13:56:03', '2018-10-13 13:56:03', null);
INSERT INTO `user_info` VALUES ('536', '1', '欧阳', 'ouyang', '123456', '1100', '2018-10-13 13:56:03', '2018-10-13 13:56:03', null);
INSERT INTO `user_info` VALUES ('537', '1', '诸葛', 'zhuge', '123456', '0011', '2018-10-13 13:56:03', '2018-10-13 13:56:03', null);
INSERT INTO `user_info` VALUES ('538', '1', '司马', 'sima', '123456', '1111', '2018-10-13 13:56:04', '2018-10-13 13:56:04', null);
