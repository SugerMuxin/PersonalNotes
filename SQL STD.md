
<span style="color:green;">1.添加新数据库</span> 
CREATE DATABASE egg;

<span style="color:green;">2. 在数据库中添加新表</span> 

USE egg;

CREATE TABLE eggs_record {
	    id INT PRIMARY KEY AUTO_INCREMENT,
		eggname VARCHAR(30) NOT NULL,
		sold DATE NULL
};

<span style="color:green;">3. 插入一组值到一个表格里</span> 

INSERT INFO egg.eggs_record ( id, eggname, sold)
VALUES (1, '鸡蛋' , ‘2025-06-07’);

INSERT INFO egg.eggs_record
VALUES (2, 'YA蛋' , ‘2025-06-07’);

INSERT INFO egg.eggs_record
VALUES (DEFAULT, '炸蛋' , NULL);

<span style="color:green;">4. 修改表项，添加新项</span> 

ALERT TABLE egg.eggs_record
ADD stock INT NULL;

<span style="color:green;">5. 更新表中的一项数据</span> 

UPDATE egg.eggs_record
SET sold='2025-06-08'
WHERE id = 3;

<span style="color:green;">6. 删除数据库中的一项数据</span> 
DELETE FROM egg.eggs_record
WHERE id='1';

<span style="color:green;">7. 删除数据库中的一个表</span> 
DROP TABLE egg.eggs_record; 

<span style="color:green;">8. 删除一个数据库</span> 
DROP DATABASE egg;

<span style="color:green;">9. 数据库查询技术</span>
SELECT distinct Country,Confirmed FROM egg_database.covid_month 
order by Confirmed asc;

distinct : 过滤掉重复的，必须放在SELECT之后
order by : 应该放在 查询的数据库最后
asc desc : 分别代表升序和降序

![[Pasted image 20250608184031.png]]
192.168.2.155


