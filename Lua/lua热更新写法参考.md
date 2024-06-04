CS.UnityEngine.Debug.LogError('Call From XLUA')

xlua.private_accessible(CS.ViewWorldObjectLogic)


xlua.hotfix(CS.ViewWorldObjectLogic, 'StartBegin', function(self)
    CS.UnityEngine.Debug.LogError('CS.ViewWorldObjectLogic NewStartBegin')

	self.uiNode.playerCityOfficialName = self:GetDataBind("myofficial").Text
	
    local buttonGroupParent = self.transform:Find("btnGroupBg")
	
	for i = 0,buttonGroupParent.childCount-1,1 do
		local child = buttonGroupParent:GetChild(i)
		if child.gameObject.activeSelf and string.find(child.name,"buttonGroup") ~= nil then
		
			for j = 0,child.childCount - 1,1 do
			
			   local childGameObj = child:GetChild(j).gameObject
			   local dataBind     = childGameObj:GetComponent(typeof(CS.DataBind))
			   
				if dataBind.moduleID == "GoldMine_atlas_battle" then
				
					CS.ViewConfirmExpeditionLogic.SetExpeditionType(CS.ViewConfirmExpeditionLogic.ExpeditionType.__CastFrom('Expedition_Other'))
					CS.LTGame.TeamExpitionController:GetInstance():SetExpeditionType(CS.LTGame.ExpeditionType.__CastFrom('Expedition_Other'))
					
					
					break;
				end
			end
		end
	end
end)



print('Hello RedAlert-----------------------------------------------')


CS.UnityEngine.Debug.LogError('Call From XLUA')

xlua.private_accessible(CS.LTGame.SeasonCombineView)
xlua.private_accessible(CS.LTGame.SeasonServerCell)

local leftArrow,rightArrow
local serverScroll
local m_beCombineGroup
local m_itemPrefab




xlua.hotfix(CS.LTGame.SeasonCombineView, 'StartBegin', function(self)
		CS.LTGame.SeasonCombineView.instance = self
        self.thisDialog = self.transform:GetComponent(typeof(CS.Framework.Dialog))
		leftArrow = self:GetDataBind("leftArrow").gameObject
		rightArrow = self:GetDataBind("rightArrow").gameObject
		local sScroll = self:GetDataBind("serverScroll")
		serverScroll = sScroll:GetComponent(typeof(CS.UnityEngine.UI.ScrollRect))
		local CombineGroup = self:GetDataBind("m_beCombineGroup")
		m_beCombineGroup = CombineGroup:GetComponent(typeof(CS.UnityEngine.UI.HorizontalLayoutGroup))
		m_itemPrefab = self:GetDataBind("m_itemPrefab").gameObject
        self:InitEvent()
        self:Init()
end)



local leftSpace,intervalSpace

function SetGroupSpace(self)	
	local seasonCombineData =  self.seasonCombineData

	if self.seasonCombineData.beCombineSers.Count == 2 then
		leftSpace = 145
		intervalSpace = 210
	elseif self.seasonCombineData.beCombineSers.Count == 3 then 
		leftSpace = 40;
        intervalSpace = 100;
	else
        leftSpace = 0;
        intervalSpace = 60;
	end
	m_beCombineGroup.padding.left = leftSpace;
    m_beCombineGroup.spacing = intervalSpace;
end

function SetS1BeCombineServers(self)
	if m_beCombineGroup == nil then		
		return
	end
	SetGroupSpace(self)
	m_beCombineGroup.transform:RemoveAllChildren()
	CS.UnityEngine.Debug.LogError(self.seasonCombineData.beCombineSers.Count)
	local call = function(a, b)		
		local cell = a:GetComponent(typeof(CS.LTGame.SeasonServerCell))
		cell:SetActive(true);
		local tempi = b;
		cell:SetCell(self.seasonCombineData.beCombineSers[tempi])
	end	
	
	SetList(self.seasonCombineData.beCombineSers.Count-1, m_beCombineGroup.transform, m_itemPrefab,call, true)
	
	local callarrow = function(value)
		CS.UnityEngine.Debug.LogError(value)
		if serverScroll.horizontalScrollbar.value == 0 then
			leftArrow:SetActive(false);
		else
			leftArrow:SetActive(true);
		end
		if serverScroll.horizontalScrollbar.value == 1 then
			rightArrow:SetActive(false);
		else
			rightArrow:SetActive(true);
		end
	end	
	if self.seasonCombineData.beCombineSers.Count > 3 then 
		serverScroll.horizontalScrollbar.onValueChanged:AddListener(callarrow)
	else
		leftArrow:SetActive(false)
        rightArrow:SetActive(false)
	end
end


xlua.hotfix(CS.LTGame.SeasonCombineView, 'FillSeasonServers', function(self)
	local seasonStage = self.seasonCombineData.seasonStage
	local beCombineSerList = {}
	local toCombineSerList = {}
	if seasonStage == 0 then
		local length = self.beCombineSerList1.Length
		for i=0,length-1 do 
			beCombineSerList[i] = self.beCombineSerList1[i]
		end
		local length1 = self.toCombineSerList1.Length
		for i=0,length1-1 do 
			toCombineSerList[i] = self.toCombineSerList1[i]
		end
		SetS1BeCombineServers(self)
		for i=0,length1-1 do
			if i< self.seasonCombineData.toCombineSers.Count then 
				toCombineSerList[i]:SetActive(true)
				local server = self.seasonCombineData.toCombineSers[i]
				toCombineSerList[i]:SetCell(server)
			else
				toCombineSerList[i]:SetActive(false)
			end
		end
		
	else
		local length = self.beCombineSerList2.Length
		for i=0,length-1 do 
			beCombineSerList[i] = self.beCombineSerList2[i]
		end
		local length1 = self.toCombineSerList2.Length
		for i=0,length1-1 do 
			toCombineSerList[i] = self.toCombineSerList2[i]
		end
		for i=0,length-1 do
            if i < self.seasonCombineData.beCombineSers.Count then
                beCombineSerList[i]:SetActive(true);
                local server = self.seasonCombineData.beCombineSers[i];
                beCombineSerList[i]:SetCell(server);
            else
                beCombineSerList[i]:SetActive(false);
            end
        end
		for i=0,length1-1 do
            if i < self.seasonCombineData.toCombineSers.Count then
                toCombineSerList[i]:SetActive(true);
                local server = self.seasonCombineData.toCombineSers[i]
                toCombineSerList[i]:SetCell(server, self.selcetIndex,function(index)	
					self:SelectCall(index)
				end)
            else
                toCombineSerList[i]:SetActive(false);
            end
        end
	end	
end)


function SetList(prefabCount,parentTran,itemPrefab,callBack,adaptNum)
	if adaptNum == true then 
		for i= prefabCount,parentTran.childCount do
			parentTran.GetChild(i):SetActive(false);
		end
	end
	for i = 0,prefabCount do
		local tempi = i
		local cell
		if tempi < parentTran.childCount then 
			cell = parentTran.GetChild(tempi):GetComponent(typeof(CS.LTGame.SeasonServerCell))
		else
		    local go = CS.UnityEngine.GameObject.Instantiate(itemPrefab)
            parentTran.transform:AddChildToTarget(go.transform);
            cell = go.transform:GetComponent(typeof(CS.LTGame.SeasonServerCell))
		end
		if cell == nil then 
			goto continue
		end
		cell:SetActive(true)
		if callBack ~= nil then
			callBack(cell, tempi)
		end
		::continue::
		print([[i'm end]])
	end
end









































