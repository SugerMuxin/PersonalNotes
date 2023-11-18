
我们都知道，类是对象的抽象，主要包含属性和方法，属性是用来描述和反映对象特征的参数，方法是用来描述对象的行为，那如果我想对类、属性、方法做进一步的描述呢？这时候我们就可以使用特性。

Unity中常用的特性：
[Space]
　　可以与上面形成一个空隙，可以带参数[Space（30）]
[Header(“XXX”)]
　　在Inspector面板上给定义的字段的上一行加段描述，可以将属性隔离开，形成分组的感觉
[Tooltip(“XXX”)]
　　给一个变量添加这个属性后,在Inspector面板将鼠标悬停在该变量上可以显示提示
[Range(min, max)]`
　　限制数值变量的取值范围并以滑动条显示在Inspector中
[Min(min)]
　　限制一个 float,int 类型的变量的最小值(测试2018,2019不能正常使用,据说2020修复了这个bug)
[SerializeField]
　　强制序列化一个私有的变量,使其可以在Inspector面板显示,很多UI都会对private的组件进行强制序列化
[NonSerialized]
　　在Inspector版面中隐藏public属性，不执行序列化
[HideInInspector]
　　使属性在Inspector中隐藏，但是还是可序列化，想赋值可以通过写程序赋值序列化
[System.Serializable]
　　使自定义的类能进行序列化，即当做一个public成员的时候可以在Inspector显示
[InspectorName(“枚举A”)]
　　标记枚举类型的枚举值，可以使枚举值在Inspector上显示的名字改变


————————————————
