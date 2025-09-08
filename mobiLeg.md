 **1. 高通（Qualcomm）Adreno GPU**
 - **特点**：集成于骁龙（Snapdragon）SoC中，性能与能效平衡，支持Vulkan、OpenGL ES等图形API。
- **代表型号**：
    - 高端：Adreno 740（骁龙8 Gen 2）、Adreno 750（骁龙8 Gen 3）
    - 中端：Adreno 642（骁龙778G）、Adreno 619（骁龙695）
----
 **2. ARM Mali GPU**

- **特点**：ARM公版设计，授权给三星、联发科等厂商，广泛用于中低端和部分高端芯片。
- **代表型号**：
    - **高端**：Mali-G715（Immortalis系列，支持硬件光追）、Mali-G710
    - **主流**：Mali-G610（天玑9200）、Mali-G68（Exynos 1380）
    - **低功耗**：Mali-G52（入门级芯片）
---
 **3. 苹果（Apple）定制GPU**
- **特点**：自研架构，集成于A系列/M系列芯片，性能领先，优化iOS/macOS生态。
- **代表型号**：
    - A17 Pro GPU（6核，支持硬件光追）
    - M2系列GPU（10核，桌面级性能）
----

**4. Imagination PowerVR GPU**
- **特点**：早期广泛用于iPhone（A系列前），现多见于物联网、车载等市场。
- **代表型号**：PowerVR AXM-8-256（少数安卓设备采用）。
    
---

 **5. 三星（Samsung）Xclipse（AMD合作）**
- **特点**：基于AMD RDNA2架构，首次在移动端支持硬件光追。
- **代表型号**：Xclipse 920（Exynos 2200）。

---

 **6. 联发科（MediaTek）**
- **特点**：多采用ARM Mali GPU，高端型号自研优化（如天玑9300的Immortalis-G720）。
- **代表型号**：Mali-G710 MP10（天玑9000+）、Immortalis-G720（天玑9300）。

 **主要区别**：

- **性能**：苹果定制GPU > 高通Adreno ≈ 三星Xclipse > ARM Mali（同代对比）。
    
- **生态**：Adreno（安卓旗舰主流）、Mali（中低端普及）、苹果（封闭优化）。
    
- **新技术**：光追支持（Xclipse、Adreno 7系、苹果A17 Pro/M系列）。