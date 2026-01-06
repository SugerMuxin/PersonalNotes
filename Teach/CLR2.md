---
author: RuoSaChen
tags:
  - teach
  - csharp
---



那些“简单的代码”


```
public static void Main() {
  Int32 v = 5;       // 创建未装箱值类型变量
  Object o = v;      // o 引用已装箱的、包含值 5 的 Int32
  v = 123;           // 将未装箱的值修改成 123

  Console.WriteLine(v + "," + (Int32)o);     // 显示 "123,5"
}
```
> 上述代码发生了多少次装箱？
> 答案： 1 次 (这里书籍中指出的有3次，但是在SharpLab中编译出的IL代码只有一次box ，可能是版本不同导致的？)

```
public static void Main() {
    Int32 v = 5;              // 创建未装箱的值类型变量
    Object o = v;             // o 引用 v 的已装箱版本

    v = 123;                  // 将未装箱的值类型修改成 123
    Console.WriteLine(v);     // 显示 “123”
    v = (Int32)o;             // 拆箱并将 o 复制到 v
    Console.WriteLine(v);     // 显示 “5”
}
```
> 上述代码发生了多少次装箱？
> 答案： 1 次



```
using System;

public sealed class Program {
  public static void Main() {
    Int32 v = 5;      // 创建未装箱的值类型变量

  #if INEFFICIENT
    // 编译下面这一行， v 被装箱 3 次，浪费时间和内存
    Console.WriteLine("{0}, {1}, {2}", v, v, v);
  #else
    // 下面的代码结果一样，但无论执行速度，还是内存利用，都比前面的代码更胜一筹
    Object o = v;     // 对 v 进行手动装箱(仅 1 次)

    // 编译下面这一行不发生装箱
    Console.WriteLine("{0}, {1}, {2}", o, o, o);
  #endif
  }
}

```
在定义了 `INEFFICIENT` 符号的前提下编译，编译器会生成代码对 `v` 装箱 3 次，造成在堆上分配 3 个对象！这太浪费了，因为每个对象都是完全相同的内容：**5**。在没有定义 `INEFFICIENT` 符号的前提下编译， `v` 只装箱一次，所以只在堆上分配一个对象。随后，在对 `Console.WriteLine` 方法的调用中，对同一个已装箱对象的引用被传递 3 次。第二个版本执行起来快得多，在堆上分配的内存也要少得多。