
KCP协议集合了TCP协议和UDP协议的优缺点，通过超时传输，快速重传和编号的确认机制来实现了可靠传输，能以比TCP浪费10%~20% 的带宽代价换取平均延时降低30%~40%。纯算法实现。


<span style="color:yellow;">TCP是为流量设计的（每秒内可以传输多少KB的数据），讲究的是充分利用带宽。而KCP是为流速设计的（单个数据从一端发送到另一端需要多少时间）</span>

https://www.zhihu.com/tardis/zm/art/112442341?source_id=1003
https://luyuhuang.tech/2020/12/09/kcp.html

TCP报文
![[Pasted image 20250122174147.png]]


UDP报文
![[Pasted image 20250122174156.png]]


KCP报文
![[Pasted image 20250122174208.png]]
![[Pasted image 20250122174237.png]]

