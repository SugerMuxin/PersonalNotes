##### 一. 创建方式
>1. 静态方法创建数据对象
```
using UnityEngine;
using UnityEditor;
public class ScriptableObjectTool 
{
    [MenuItem("ScritableObject/CreateMyData")]
    public static void CreateMyData()
    {
        //创建数据资源文件
        //泛型是继承自ScriptableObject的类
        BulletData asset = ScriptableObject.CreateInstance<BulletData>();
        //前一步创建的资源只是存在内存中，现在要把它保存到本地
        //通过编辑器API，创建一个数据资源文件，第二个参数为资源文件在Assets目录下的路径
        AssetDatabase.CreateAsset(asset, "Assets/Resources/ScriptableObject/BulletData.asset");
        //保存创建的资源
        AssetDatabase.SaveAssets();
        //刷新界面
        AssetDatabase.Refresh();
    }
}

```

>2.为类型添加 CreateAssetMenu属性来创建

```
[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObject/子弹数据", order = 0)]
public class BulletData : ScriptableObject
{
    public float speed;
    public float damage;
}

```

二、使用方法

>1.和Prefab对象一样，可以直接拖到Inspector面板上
![[Pasted image 20240531144554.png]]

>2.直接加载数据资源
可以用 Resources，AddressBundle，Addressables 等方式加载数据资源文件。ScriptableObject 和MonoBehaviour 类似，也存在生命周期函数，但是数量会少很多。除此之外继承自 ScriptableObject 的类中也可以自定义函数，并不是只能声明和数据有关的变量。
```
	Awake 数据文件创建时调用
	OnDestroy 对象将被销毁时调用
	OnEnable 创建或加载对象时调用
	OnDisable 对象销毁时，即将加载脚本程序集时调用
	OnValidate 编辑器才会调用的函数，Unity在加载脚本或者Inspector面板中更改值时调用
```

3.可以动态的创建数据实例
这样做的好处是不必要存储空间，只需要在运行时初始化一个T类型的动态持久化内存资源。关闭时销毁，这种方式可以使用ScriptableObject的动态