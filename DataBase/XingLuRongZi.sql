/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2018/9/19 23:25:50                           */
/*==============================================================*/


drop table if exists menueditor_info;

drop table if exists project_info;

drop table if exists projectzijin_userinfo;

drop table if exists rongzi_info;

drop table if exists rongzianpai_info;

drop table if exists rongzihuanbenfuxi_info;

drop table if exists rongzitiaozheng_info;

drop table if exists rongzizhihuan_info;

drop table if exists user_info;

drop table if exists userlog_info;

/*==============================================================*/
/* Table: menueditor_info                                       */
/*==============================================================*/
create table menueditor_info
(
   idstr                int not null auto_increment,
   primary key (idstr)
);

/*==============================================================*/
/* Table: project_info                                          */
/*==============================================================*/
create table project_info
(
   idstr                int(20) not null auto_increment,
   projectname          varchar(100),
   projectdanwei        varchar(100),
   projectzongtou       decimal(32,2),
   projectstartdate     date,
   projectcompletedate  date,
   prpjecttype          varchar(40),
   projectstatus        varchar(40),
   createtime           datetime,
   remark               varchar(200),
   primary key (idstr)
);

/*==============================================================*/
/* Table: projectzijin_userinfo                                 */
/*==============================================================*/
create table projectzijin_userinfo
(
   idstr                int(20) not null auto_increment,
   lururen              varchar(100),
   lurudate             date,
   projectname          varchar(100),
   zhaiwuname           varchar(100),
   zhaiwujine           dec(32,2),
   zhifuxingzhi         varchar(40),
   zhifudate            date,
   zhifujine            dec(32,2),
   shouhuidate          date,
   shouhuijine          dec(32,2),
   remark               varchar(100),
   createtime           datetime,
   primary key (idstr)
);

/*==============================================================*/
/* Table: rongzi_info                                           */
/*==============================================================*/
create table rongzi_info
(
   idstr                int(20) not null auto_increment,
   zhaiwunrename        varchar(100),
   zhaiquanrenname      varchar(100),
   zhaiquanrentype      varchar(40),
   rongzifangshi        varchar(40),
   zhaiwutype           varchar(40),
   zhaiwuname           varchar(100),
   zijinyongtu          varchar(100),
   hetongjine           decimal(32,2),
   daoweijineshidian    datetime,
   daoweijine           decimal(32,2),
   rongziyueshidian     datetime,
   danbaofangshi        varchar(40),
   lilvtype             varchar(40),
   lilvmiaoshu          varchar(40),
   lilvbaifenbi         double(4,2),
   feilv                double(4,2),
   qixian               int,
   qixiri               date,
   daoqiri              date,
   remark               varchar(200),
   hetongno             varchar(100),
   createtime           datetime,
   rongziyue            decimal(32,2),
   primary key (idstr)
);

/*==============================================================*/
/* Table: rongzianpai_info                                      */
/*==============================================================*/
create table rongzianpai_info
(
   idstr                int(20) not null auto_increment,
   luruname             varchar(100),
   lurudate             date,
   zhaiwuname           varchar(100),
   projectname          varchar(100),
   anpaijine1           decimal(32,2),
   anpaijine2           decimal(32,2),
   anpaijine3           decimal(32,2),
   anpaidate            date,
   yuedate              date,
   remark               varchar(100),
   createtime           datetime,
   primary key (idstr)
);

/*==============================================================*/
/* Table: rongzihuanbenfuxi_info                                */
/*==============================================================*/
create table rongzihuanbenfuxi_info
(
   idstr                int(20) not null auto_increment,
   fuxiyear             int,
   fuxidatetype         varchar(100),
   fuxidate             date,
   jixitianshu          int,
   nianlilv             double(4,2),
   feilv                double(4,2),
   daozhangjine         decimal(32,2),
   huanbenjine          decimal(32,2),
   benjinyue            decimal(32,2),
   yingjilixi           decimal(32,2),
   yingfufeiyong        decimal(32,2),
   shifouqingchang      int,
   remark               varchar(200),
   createtime           datetime,
   primary key (idstr)
);

/*==============================================================*/
/* Table: rongzitiaozheng_info                                  */
/*==============================================================*/
create table rongzitiaozheng_info
(
   idstr                int(20) not null auto_increment,
   luruname             varchar(100),
   lurudate             date,
   zijinlaiyuan         varchar(100),
   projectnamebefore    varchar(100),
   projectjinebefore    decimal(32,2),
   projectnamenow       varchar(100),
   projectjinenow       decimal(32,2),
   remark               varchar(200),
   createtime           datetime,
   primary key (idstr)
);

/*==============================================================*/
/* Table: rongzizhihuan_info                                    */
/*==============================================================*/
create table rongzizhihuan_info
(
   idstr                int(20) not null auto_increment,
   luruname             varchar(100),
   lurutime             time,
   zhihuanjine          decimal(32,2),
   zhihuantime          time,
   zhihuannamebefore    varchar(100),
   zhihuannamenow       varchar(100),
   remark               varchar(200),
   createtime           datetime,
   primary key (idstr)
);

/*==============================================================*/
/* Table: user_info                                             */
/*==============================================================*/
create table user_info
(
   idstr                int(20) not null auto_increment,
   rodetype             int,
   username             varchar(100),
   loginname            varchar(100),
   loginpass            varchar(100),
   rolequanxian         varchar(32),
   createtime           datetime,
   updatetime           datetime,
   remark               varchar(100),
   primary key (idstr)
);

/*==============================================================*/
/* Table: userlog_info                                          */
/*==============================================================*/
create table userlog_info
(
   idstr                int(20) not null auto_increment,
   loginname            varchar(100),
   logtype              int,
   logcontent           varchar(512),
   createtime           datetime,
   primary key (idstr)
);

