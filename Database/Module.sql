
insert into SYS_MODULE (ID, PARENT_ID, CODE, NAME, MODULE_PATH, FUNCTION_PATH, CREATE_DATE, LAST_UPDATE_DATE)
values ('07E4DD167F29C04EB2D769B042955FA0', null, '00010001', '系统设置', null, null, to_date('25-01-2019 17:56:14', 'dd-mm-yyyy hh24:mi:ss'), to_date('25-01-2019 17:56:14', 'dd-mm-yyyy hh24:mi:ss'));

insert into SYS_MODULE (ID, PARENT_ID, CODE, NAME, MODULE_PATH, FUNCTION_PATH, CREATE_DATE, LAST_UPDATE_DATE)
values ('3C37088D7624D843B66750EE94B30F58', '07E4DD167F29C04EB2D769B042955FA0', '000100010001', '用户管理', 'userrole', 'user', to_date('25-01-2019 18:06:18', 'dd-mm-yyyy hh24:mi:ss'), to_date('25-01-2019 18:06:18', 'dd-mm-yyyy hh24:mi:ss'));
