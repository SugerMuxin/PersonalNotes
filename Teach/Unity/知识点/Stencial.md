### 为什么要叫Stencial


##### stencial 与 depth

```
	Stencial{
		//当前像素 stencial 值与 0 进行比较
		Ref 0              //0-255
		//测试条件:测试是否相等
		Comp Equal       //default:always
		//如果测试通过对此stencial值进行写入操作：保持当前stencial值
		Pass keep          //default:keep
		//如果测试失败对此stencial值进行写入操作：保持当前stencial
		Fail keep          //default:keep
		//如果深度测试失败对此stencial值进行的写入操作：循环递增
		ZFail IncrWrap     //defalut:keep
	}

```

```
	Stencial{
		//当前像素 stencial 值与 0 进行比较
		Ref 0              //0-255
		//测试条件:测试是否相等
		Comp Equal       //default:always
		//如果测试通过对此stencial值进行写入操作：保持当前stencial值
		Pass keep          //default:keep
		//如果测试失败对此stencial值进行写入操作：保持当前stencial
		Fail keep          //default:keep
		//如果深度测试失败对此stencial值进行的写入操作：循环递增
		ZFail IncrWrap     //defalut:keep
	}

```

```
	Stencial{
		//当前像素 stencial 值与 0 进行比较
		Ref 0              //0-255
		//测试条件:测试是否相等
		Comp Equal       //default:always
		//如果测试通过对此stencial值进行写入操作：保持当前stencial值
		Pass keep          //default:keep
		//如果测试失败对此stencial值进行写入操作：保持当前stencial
		Fail keep          //default:keep
		//如果深度测试失败对此stencial值进行的写入操作：循环递增
		ZFail IncrWrap     //defalut:keep
	}

```



