>1. frac 取小数部分
    floor 是向下取整，frac是取小数部分

>2. saturate函数
> 	将颜色规范到 0~1 之间
	if (any(saturate(v.texcoord) - v.texcoord)) {
		o.color.b = 0.5;
	}

