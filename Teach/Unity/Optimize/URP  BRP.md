1. 在BRP中渲染一个点需要三个批次，如果使用URP只需要两个批次。That's because URP doesn't use a separate depth pass for directional shadows. It does have more set-pass calls, but that doesn't appear to be a problem.
https://catlikecoding.com/unity/tutorials/basics/measuring-performance/


2. URP 的Dynamic batching 对阴影并不生效，这就是为啥在URP中使用dynamic batching的效果不如 BRP中的 db的原因。而GPU Instancing 却会表现优异
 ![[Pasted image 20250714151022.png]]



