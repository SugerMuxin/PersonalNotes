

1. 

2. 使用默认类型转换
在编码中我们会频繁的使用 string,int,float 等之间的相互的转换，尤其是在配置表到对象的实例化的过程中，
那么我们如何进行高效的类型转换就成了一个需要注意的问题。通常得用的帮助类提供的方法，如下：
```
string strValue ="100";
int intValue = 0;
int.TryParse(strValue,out intValue);

string str = intValue.ToString();

double doubleValue = intValue.ToDouble();

```

