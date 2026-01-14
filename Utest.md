# 图文混排布局示例

<style>
/* 全局样式 */
.container {
    max-width: 1200px;
    margin: 0 auto;
    padding: 20px;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    line-height: 1.6;
    color: #333;
}

.section {
    margin: 40px 0;
    padding: 20px;
    background: #f8f9fa;
    border-radius: 10px;
    box-shadow: 0 2px 8px rgba(0,0,0,0.1);
}

.section h2 {
    color: #2c3e50;
    border-bottom: 2px solid #3498db;
    padding-bottom: 10px;
    margin-top: 0;
}

/* 方法1：浮动布局 */
.float-container {
    overflow: hidden;
    margin: 20px 0;
}

.float-image {
    float: right;
    width: 300px;
    margin: 0 0 20px 20px;
    border-radius: 8px;
    box-shadow: 0 4px 12px rgba(0,0,0,0.15);
    transition: transform 0.3s ease;
}

.float-image:hover {
    transform: translateY(-5px);
}

.float-image img {
    width: 100%;
    height: auto;
    border-radius: 8px;
}

.image-caption {
    text-align: center;
    color: #666;
    font-size: 0.9em;
    margin-top: 8px;
    font-style: italic;
}

/* 方法2：Flexbox布局 */
.flex-container {
    display: flex;
    flex-wrap: wrap;
    gap: 30px;
    margin: 20px 0;
    align-items: flex-start;
}

.flex-text {
    flex: 1;
    min-width: 300px;
}

.flex-image-wrapper {
    width: 300px;
    flex-shrink: 0;
}

.flex-image {
    width: 100%;
    border-radius: 8px;
    box-shadow: 0 4px 12px rgba(0,0,0,0.15);
}

/* 方法3：Grid布局 */
.grid-container {
    display: grid;
    grid-template-columns: 1fr auto;
    gap: 30px;
    margin: 20px 0;
    align-items: start;
}

.grid-text {
    grid-column: 1;
}

.grid-image-wrapper {
    grid-column: 2;
    width: 300px;
}

.grid-image {
    width: 100%;
    border-radius: 8px;
    box-shadow: 0 4px 12px rgba(0,0,0,0.15);
}

/* 方法4：形状环绕 */
.shape-container {
    position: relative;
    margin: 20px 0;
}

.shape-image {
    float: right;
    shape-outside: polygon(0 0, 100% 0, 100% 100%, 0 100%);
    shape-margin: 20px;
    width: 280px;
    margin: 0 0 20px 20px;
}

.shape-image img {
    width: 100%;
    border-radius: 8px;
    box-shadow: 0 4px 12px rgba(0,0,0,0.15);
}

/* 响应式设计 */
@media (max-width: 768px) {
    .flex-container {
        flex-direction: column;
    }
    
    .grid-container {
        grid-template-columns: 1fr;
    }
    
    .grid-image-wrapper {
        grid-column: 1;
        width: 100%;
    }
    
    .float-image,
    .flex-image-wrapper,
    .shape-image {
        width: 100%;
        float: none;
        margin: 0 0 20px 0;
    }
}

/* 清除浮动 */
.clearfix::after {
    content: "";
    display: table;
    clear: both;
}

/* 代码块样式 */
pre {
    background: #2d3748;
    color: #e2e8f0;
    padding: 20px;
    border-radius: 8px;
    overflow-x: auto;
    font-size: 14px;
    margin: 20px 0;
}

code {
    background: #edf2f7;
    padding: 2px 6px;
    border-radius: 4px;
    font-family: 'Courier New', Courier, monospace;
}

/* 按钮样式 */
.btn {
    display: inline-block;
    padding: 10px 20px;
    background: #3498db;
    color: white;
    text-decoration: none;
    border-radius: 5px;
    margin: 10px 5px;
    transition: background 0.3s;
}

.btn:hover {
    background: #2980b9;
}
</style>

<div class="container">
  <h1>CSS图文混排布局示例</h1>
  
  <p>本示例展示了在Markdown中使用CSS实现图文混排的多种方法。每种方法都有其特点，适用于不同的场景。</p>
  
  <!-- 方法1：浮动布局 -->
  <section class="section">
    <h2>方法一：浮动布局 (Float)</h2>
    
    <div class="float-container clearfix">
      <div class="float-image">
        <img src="https://images.unsplash.com/photo-1506744038136-46273834b3fb" alt="山水风景">
        <div class="image-caption">美丽的山水风景 - 浮动在右侧</div>
      </div>
      
      <p><strong>浮动布局</strong>是CSS中最经典的图文混排方式。通过设置图片<code>float: right</code>，可以使文字自然环绕在图片周围。</p>
      
      <p>这种方法兼容性极好，从早期的浏览器到现代浏览器都支持。当文字内容超过图片高度后，文字会自动在图片下方以整行宽度继续排列。</p>
      
      <p>使用浮动布局时需要注意清除浮动，防止影响后续元素的布局。在这个例子中，我们使用了<code>.clearfix</code>类来确保容器正确包含浮动元素。</p>
      
      <p>浮动的优势在于简单直接，但在复杂的响应式布局中可能不如Flexbox或Grid灵活。不过对于简单的图文混排场景，它仍然是一个非常可靠的选择。</p>
      
      <p>你可以通过调整边距、宽度和浮动方向来控制布局效果。例如，要实现左侧图片右侧文字，只需将<code>float: right</code>改为<code>float: left</code>即可。</p>
    </div>
    
    <div class="code-example">
      <h3>实现代码：</h3>
      <pre><code>&lt;div class="float-container clearfix"&gt;
  &lt;div class="float-image"&gt;
    &lt;img src="image.jpg" alt="描述"&gt;
    &lt;div class="image-caption"&gt;图片说明&lt;/div&gt;
  &lt;/div&gt;
  &lt;p&gt;这里是文字内容...&lt;/p&gt;
&lt;/div&gt;</code></pre>
    </div>
  </section>
  
  <!-- 方法2：Flexbox布局 -->
  <section class="section">
    <h2>方法二：Flexbox布局</h2>
    
    <div class="flex-container">
      <div class="flex-text">
        <p><strong>Flexbox</strong>是现代CSS布局的强大工具，特别适合一维布局（行或列）。</p>
        
        <p>通过设置容器为<code>display: flex</code>，我们可以创建灵活的图文排列。文字区域使用<code>flex: 1</code>来占据剩余空间，而图片区域则设置固定宽度。</p>
        
        <p>Flexbox的优势在于其强大的对齐能力和空间分配机制。我们可以轻松控制项目在主轴和交叉轴上的对齐方式，创建复杂的响应式布局。</p>
        
        <p>当文字内容超过图片高度时，文字区域会自然扩展，而图片区域保持固定大小。这确保了布局的稳定性和可预测性。</p>
        
        <p>Flexbox还支持<code>flex-wrap</code>属性，当空间不足时可以将项目换行显示。在移动设备上，我们可以轻松地将布局从行方向改为列方向。</p>
        
        <p>对于需要复杂对齐和空间分配的布局，Flexbox通常是更好的选择。它的学习曲线比浮动更平缓，功能也更强大。</p>
      </div>
      
      <div class="flex-image-wrapper">
        <img class="flex-image" src="https://images.unsplash.com/photo-1519681393784-d120267933ba" alt="星空">
        <div class="image-caption">璀璨星空 - Flexbox布局</div>
      </div>
    </div>
    
    <div class="code-example">
      <h3>实现代码：</h3>
      <pre><code>&lt;div class="flex-container"&gt;
  &lt;div class="flex-text"&gt;
    &lt;p&gt;这里是文字内容...&lt;/p&gt;
  &lt;/div&gt;
  &lt;div class="flex-image-wrapper"&gt;
    &lt;img class="flex-image" src="image.jpg" alt="描述"&gt;
    &lt;div class="image-caption"&gt;图片说明&lt;/div&gt;
  &lt;/div&gt;
&lt;/div&gt;</code></pre>
    </div>
  </section>
  
  <!-- 方法3：Grid布局 -->
  <section class="section">
    <h2>方法三：Grid布局</h2>
    
    <div class="grid-container">
      <div class="grid-text">
        <p><strong>CSS Grid</strong>是二维布局系统，可以同时处理行和列的布局。</p>
        
        <p>通过定义网格模板列<code>grid-template-columns: 1fr auto</code>，我们创建了一个两列布局：第一列占据剩余空间(<code>1fr</code>)，第二列根据内容自动调整宽度。</p>
        
        <p>Grid布局提供了最精确的布局控制能力。我们可以定义网格线、网格区域，以及项目在网格中的位置和大小。</p>
        
        <p>与Flexbox不同，Grid专门为二维布局设计。对于复杂的杂志式布局、仪表盘或任何需要精确控制行列关系的场景，Grid是最佳选择。</p>
        
        <p>在这个例子中，文字区域和图片区域分别占据不同的网格列。文字区域会自动扩展以适应内容，而图片区域保持固定宽度。</p>
        
        <p>Grid布局的另一个优势是能够轻松创建响应式设计。通过媒体查询调整网格模板，我们可以为不同屏幕尺寸创建完全不同的布局结构。</p>
      </div>
      
      <div class="grid-image-wrapper">
        <img class="grid-image" src="https://images.unsplash.com/photo-1518837695005-2083093ee35b" alt="城市夜景">
        <div class="image-caption">城市夜景 - Grid布局</div>
      </div>
    </div>
    
    <div class="code-example">
      <h3>实现代码：</h3>
      <pre><code>&lt;div class="grid-container"&gt;
  &lt;div class="grid-text"&gt;
    &lt;p&gt;这里是文字内容...&lt;/p&gt;
  &lt;/div&gt;
  &lt;div class="grid-image-wrapper"&gt;
    &lt;img class="grid-image" src="image.jpg" alt="描述"&gt;
    &lt;div class="image-caption"&gt;图片说明&lt;/div&gt;
  &lt;/div&gt;
&lt;/div&gt;</code></pre>
    </div>
  </section>
  
  <!-- 方法4：形状环绕 -->
  <section class="section">
    <h2>方法四：形状环绕 (Shape-Outside)</h2>
    
    <div class="shape-container clearfix">
      <div class="shape-image">
        <img src="https://images.unsplash.com/photo-1519996529931-28324d5a630e" alt="沙漠">
        <div class="image-caption">沙漠景观 - 形状环绕</div>
      </div>
      
      <p><strong>形状环绕</strong>是CSS中更高级的图文混排技术，通过<code>shape-outside</code>属性可以让文字沿着非矩形形状排列。</p>
      
      <p>这种方法可以创建更自然、更杂志化的排版效果。文字会沿着定义的形状边缘流动，而不是简单的矩形框。</p>
      
      <p><code>shape-outside</code>属性支持多种形状函数：<code>circle()</code>、<code>ellipse()</code>、<code>polygon()</code>等。你甚至可以基于图像的alpha通道创建形状。</p>
      
      <p>在这个例子中，我们使用简单的矩形形状，但你可以创建更复杂的形状。例如，对于圆形图片，可以使用<code>shape-outside: circle(50%)</code>让文字沿着圆形边缘排列。</p>
      
      <p>形状环绕通常与浮动结合使用。图片需要浮动，然后通过<code>shape-outside</code>定义文字应该环绕的形状。</p>
      
      <p>这种方法在现代浏览器中得到了良好支持，但在非常旧的浏览器中可能需要降级方案。对于需要创意排版的网站，形状环绕是一个强大的工具。</p>
      
      <p>除了<code>shape-outside</code>，还可以使用<code>shape-margin</code>来控制形状与文字之间的间距，创建更舒适的阅读体验。</p>
    </div>
    
    <div class="code-example">
      <h3>实现代码：</h3>
      <pre><code>&lt;div class="shape-container clearfix"&gt;
  &lt;div class="shape-image"&gt;
    &lt;img src="image.jpg" alt="描述"&gt;
    &lt;div class="image-caption"&gt;图片说明&lt;/div&gt;
  &lt;/div&gt;
  &lt;p&gt;这里是文字内容...&lt;/p&gt;
&lt;/div&gt;</code></pre>
    </div>
  </section>
  
  <!-- 总结 -->
  <section class="section">
    <h2>方法比较与总结</h2>
    
    <table>
      <thead>
        <tr>
          <th>方法</th>
          <th>优点</th>
          <th>缺点</th>
          <th>适用场景</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td><strong>浮动布局</strong></td>
          <td>兼容性好，简单易用</td>
          <td>需要清除浮动，灵活性有限</td>
          <td>简单图文混排，兼容旧浏览器</td>
        </tr>
        <tr>
          <td><strong>Flexbox</strong></td>
          <td>强大的对齐能力，响应式设计友好</td>
          <td>一维布局，不适合复杂二维布局</td>
          <td>需要对齐控制，单方向布局</td>
        </tr>
        <tr>
          <td><strong>Grid</strong></td>
          <td>二维布局控制，精确的网格系统</td>
          <td>学习曲线较陡，旧浏览器支持有限</td>
          <td>复杂布局，杂志式排版</td>
        </tr>
        <tr>
          <td><strong>形状环绕</strong></td>
          <td>创意排版，自然文字流</td>
          <td>浏览器支持有限，需要浮动配合</td>
          <td>创意设计，杂志风格网站</td>
        </tr>
      </tbody>
    </table>
    
    <h3>选择建议：</h3>
    <ul>
      <li>如果只需要简单的图文混排，且需要支持旧浏览器，选择<strong>浮动布局</strong></li>
      <li>如果需要灵活的对齐和响应式设计，选择<strong>Flexbox</strong></li>
      <li>如果需要复杂的二维布局，选择<strong>Grid</strong></li>
      <li>如果需要创意性的文字环绕效果，选择<strong>形状环绕</strong></li>
    </ul>
    
    <h3>响应式设计要点：</h3>
    <p>无论选择哪种方法，都应该考虑响应式设计。在移动设备上，通常会将图片和文字改为垂直排列，以提高可读性。上面的示例都包含了基本的响应式调整。</p>
  </section>
  
  <footer>
    <p>© 2023 图文混排示例 | 使用Unsplash图片</p>
    <div>
      <a href="#" class="btn">返回顶部</a>
      <a href="#" class="btn">了解更多</a>
    </div>
  </footer>
</div>