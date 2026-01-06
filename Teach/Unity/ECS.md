---
author: RuoSaChen
tags:
  - teach
---

当面对一个新的事物、技术或是框架的时候。我们一般条件反射性迸发出来的几个问题是：
1. 这东西是啥？能干什么？
2. 这东西为什么会诞生？是不是为了解决什么已有的经验所解决不了的问题。或者说优势在哪？

这样的提问或是思考是可以适用于任何新的事物的诞生的。往大的说牛顿的万有引力奠定了近代物理学大厦的基础，原因就是在文艺复兴之后陆续出现了许多已有的经验所不能解释的问题。爱因斯坦的广义相对论的诞生也同样是为了解释牛顿的经典物理学框架所解释不通的引力场问题。

那么接下来第一个问题是ECS是啥？

Entity Component System (ECS) 是一个 gameplay 层面的框架，它是建立在渲染引擎、物理引擎之上的，主要解决的问题是如何建立一个模型来处理游戏对象 (Game Object) 的更新操作。传统的很多游戏引擎是基于面向对象来设计的，游戏中的东西都是对象，每个对象有一个叫做 Update 的方法，框架遍历所有的对象，依次调用其Update 方法。有些引擎甚至定义了多种 Update 方法，在同一帧的不同时机去调用。

面向对象的方式来思考问题其实很符合我们对自然的理解，一个角色作为一个父对象，下面拥有皮肤对象和武器对象等组件是一件非常合理的事情，对于这个对象的生命周期的管理也是非常自然。

那么ECS框架又是怎么来区分对象和逻辑的呢。
Entity是实体的意思，可以理解为一个实例化的GameObject对象，拥有唯一的 ID，这个实体的主要作用是用来作为一些组件的黏合，可以没有任何的功能。
```
public class RoleEntity:Entity{

}
```

Component 组件，这个组件可以理解为一个个积木，比如一个角色可以由身体组件（BodyComponen）和武器组件（WeaponComponent）组成。身体组件只是用于角色的形象展现

```
public class BodyComponent: Entity  
{  
}

public class WeaponComponent : Entity{

}
```

Refrences
1. https://blog.codingnow.com/2017/06/overwatch_ecs.html
2. https://www.lfzxb.top/ow-gdc-gameplay-architecture-and-netcode/


