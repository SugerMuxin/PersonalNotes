---
author: RuoSaChen
tags:
  - unity
  - teach
  - shader
---

1. Hierarchy中的顺序是否会影响渲染顺序？

	 答案：几乎没有影响，Hierarchy 视图中的顺序会给我们一种对象在排序的感觉，感觉多少会有点影响权重，其实Hierarchy 视图的顺序会影响 Unity收集渲染对象的顺序，但是最终渲染的顺序是按照 材质，Z排序，渲染队列等诸多属性来决定的。有点类似于学校里开运动会，你们教室在哪个位置不会影响你最终去参加跑步还是垒球，这些都是按照你本身参与的项目去决定的，甚至你在第几道起跑都已经提前决定好了，而并不是你离操场最近就让你先跑。

	那么有另外一个问题，假如有四个对象，使用完全相同的属性，包括位置大小和坐标，使用的材质等等都完全相同，他们看上去的区别只是在 Hierarchy 中的顺序是不同的，那么此时这个Hierarchy 视图中顺序会影响渲染顺序吗？展示如下图 ：  ![[Pasted image 20251001074709.png]]

	如果按照Hierarchy中的排序，那渲染顺序应该是 green->red->blue->yellow 或者是 yellow->blue ->red ->green ,那么到底是不是？我们打开FrameDebug看一下，发现不是这个顺序，如果再多加几个对象以后会发现不说一点规则没有，简直毫无关联。
	![[Pasted image 20251001075358.png]]

那么到底是按照什么排序呢？无论怎么排，总得有个排序的规则，哪怕是按照出生时间，总得有个顺序？答案是HASH码，我们先试用简单的脚本输出四个对象的Hash码。看下图就一目了然了

```
//使用简单脚本将对象的Hash输出
public class RenderOrderTest : MonoBehaviour
{
    public GameObject[] renderGameobjects;

    void Start()
    {
        for (int i = 0; i < renderGameobjects.Length; i++)
        {
            Debug.Log($"{renderGameobjects[i].name} hash: {renderGameobjects[i].GetHashCode()}");
        }
    }

}

```

![[Pasted image 20251001075111.png]]
><span style="color:red;">Tip: 这里有个问题是 对象的Instance ID 和 Hash非常相像，但是却不完全相同，偶尔测试的顺序却不与HASH的排序相同，原因暂且不知道，待补充</span>
>
><span style="color:red;">Tip: 还有一个需要注意点是，如果你在场景里添加了一个对象之后最好重启，否则你得到的Hash和Instance ID都将是一个负值（如下图），可能是因为Unity 为了保护现有对象的保护机制吧？这里我也没搞清楚。但是重启重启一次Unity之后所有的对象的Instance ID和 Hash码都正常了。就像下图这样，后面新建材质也是一样，也需要重启才能正确</span>
![[Pasted image 20251001080650.png]]


2. 在第一个问题的基础之上，如果我将四个对象使用不同的材质，这四个材质都使用StandardShader，只将四个材质上的颜色修改，然后分别适配到四个颜色的Quad上，那么渲染顺序会发生改变吗？或者说会不会按照上述对象的Hash排序？

	经过上面的验证我们很容易想到会不会是按照材质的HashCode 来排序的呢？那么我们就来新建四个不同的材质（分别为blue,red,yellow,green）.mat  分别匹配到四个不同的物体上，此处需要注意一下要重启一下Unity 修改一下代码来输出四个材质的Hash码，代码如下：

```
public class RenderOrderTest : MonoBehaviour
{
    public Material[] Materials;
    void Start()
    {
        for (int i = 0; i < Materials.Length; i++)
        {
            Debug.Log($"material {Materials[i].name} hash: {Materials[i].GetHashCode()}");
        }
    }
}
```

	得到的渲染顺序的确如猜测的顺序一样，是按照Hash顺序排序的。

   ![[Pasted image 20251001121257.png]]

--------------------------------------------------------------------------
><span style="color:green;">Tip: 在这里我们应该清楚一点的是渲染的顺序和最终显示的片元顺序是两回事，最终我们看到的结果是根据模拟物理世界的规则和我们在Shader中所定义的魔法所决定的。但是渲染的顺序只是决定谁先尝试着色，但是并不一定是最终的结果。 </span>
><span style="color:gray;">Tip: 事实上当 Hierarchy 视图中脚本执行的顺序也是按照Hash顺序执行的(也就能推测Unity的主线流程就是按照Hash的顺序执行的)</span>
---------------------------------------------

3.当项目中存在两个相机的时候会不会影响渲染顺序的改变？
	渲染顺序是按照相机的Hash决定的，从而在FrameDebug中可以看到hash值小的相机所能拍到的物体会被先绘制。当然两个相机下的object是没办法合批的，所以如果场景中存在多个相机一定要做好管理。
4.在同一个相机中不同层会不会导致绘制顺序和渲染结果的不同呢？
	接下来将Red.mat 和 Blue.mat 的材质修改为Water层，而 green.mat 和 yellow.mat 的材质的层级就保持不变了，然后我们会发现渲染顺序并不会发生改变，而且当我们将四个对象在xy轴上做些许偏移会发现渲染结果的顺序也不会受到层级的顺序影响。由此可以猜测层的作用是为了对游戏中对象进行管理。
	![[Pasted image 20251007191312.png]]

5. 有没有什么办法能让渲染顺序按照我们想要的方式去绘制？ 然后我们在每个对象上添加一个SortingGroup 来管理这几个对象，保持对象的层级均为 Default层不动，将Orderlayer 的层级分别修改为1，2，3，4，我们会发现视图中的绘制顺序发生了改变使用SortingGroup 来对对象进行排序和绘制结果都发生了改变，是按照我们想要的顺序来进行绘制的。
	![[Pasted image 20251007191930.png]]
   现在我们使用的是不同的材质，自然是不能进行合批的，然后我们将这些对象的材质都换成同一个材质，检查是否能够合批？
	   ![[Pasted image 20251008104341.png]]
	从上图可以看出，即便使用了相同的材质也不能合并批次？之所以没有合批的原因是没有开启Dynamic Batching 或者 开启 GPU Instancing。那么随之而来的另外一个问题是既然Dynamic Batching 和 GPU Instancing 都能合批，那么他们的区别是什么？我们又该如何选择呢？或者能不能都开启？都开启之后又会有什么影响？
	
	![[Pasted image 20251008105314.png]]
	
	（1）区别：Dynamic Batching 主要是在CPU上对使用了相同材质的 Mesh 进行合并，然后将合并之后的 Mesh信息传入到GPU端进行绘制，很显然如果当需要合并的 Mesh 比较复杂的时候就不适合这种合批方式了，对CPU端的压力比较大，Unity对动态合批的顶点数是有限制的，默认的是300个顶点一下的 Mesh ，如果这个Mesh还需要传递其他信息诸如切线和法线等信息的时候这个阈值还会降低。
	--   GPU Instancing 的工作原理是将同一个Mesh和材质的实例信息通过一次Draw Call 提交给GPU，同时通过实例缓冲区（Instance Buffer）传递每个实例的变换矩阵和材质属性（如颜色和 UV 偏移等）
	（2）选择，根据区别中的描述我们已经可以知道应该根据Mesh的复杂程度来决定是使用GPU Instancing还是动态合批
	（3）如果都开启的情况呢？在都开启的情况下，会优先进行GPU Instancing
