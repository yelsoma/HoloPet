專案 "HoloPet" 主要架構

1.遊戲物件

 Hierarchy上的物件(Botan,Cart,Chair...)都有各自的"StateMachine和"States"腳本。
每個物件上的"狀態機"(StateMachine)和"狀態"(States)腳本，都是繼承"StateMachineBase"和"StateBase"腳本。(腳本位置在 Assets/Scripts/State&Machine/FiniteStateMachineBase)

例如"Cart"物件上有:
"CartStateMachine" (Cart的狀態機腳本)        
"CartState_Fall"   (Cart的掉落狀態腳本)
"CartState_Idle"   (Cart的發呆狀態腳本)
"CartState_Grab"   (Cart的被選取狀態腳本)
(腳本位置都在 Assets/Scripts/State&Machine/CartStates )

 物件永遠會有一個正在執行的狀態，當物件生成時便會透過狀態機進入該物件的第一個狀態。並在達成一定條件時轉換到下一個狀態。
物件的"狀態機"腳本上面會可能進行"狀態"腳本的referance。
 
例如"Cart"的狀態機 "CartStateMachine"的第一個狀態是 "CartState_Spawn"(生成狀態)。 
生成狀態會設定一些初始值，然後直接進入"CartState_Idle"(發呆狀態)，進入發呆狀態後會持續進行地表檢測 CheckIsBotBoundery()，如果不在地表上則進入"CartState_Fall"掉落狀態，掉落到了地表上又會回到發呆狀態。



2.狀態運行方法

所有"狀態"腳本都有幾個繼承 StateBase的方法
public override void Enter(){};  進入狀態時要執行一次的方法
public override void StateUpdate(){};  狀態中要持續執行的方法
public override void Exit(){};   離開狀態時要執行一次的方法

這些方法會在"狀態機"繼承"StateMachineBase"中的
private void Start(){};
private void Update();
public void ChangeState(StateBase newState){};
裡面運行

改變狀態的條件有幾種可能
 一，狀態執行完畢(ex: State_Spawn 設定完初始值後，就結束直接進入State_Idle )
 二，狀態執行中達到條件(ex: State_Fall 會不斷掉落和 GroundCheck，GroundCheck成功後就進入 State_Idle )
 三，受到外部直接改變狀態(ex: 再任意狀態下，物件被滑鼠點擊拖曳，都會進入 State_Grab 被拖曳狀態)
第三種改變狀態的方式就會需要透過 Managers 來控制。

例如 Cart物件 的滑鼠點擊是透過 "CartInputManager" 控制。 (腳本位置在 Assets/Scripts/ObjectManagers/ObjectManagers/CartManager)
點擊滑鼠時，Hierarchy 上 GameControllCenter 中的 InputManagerCenter 會找到被點擊物件的 iClickable interface ("CartInputManager") 然後物件的 iClickable 就會直接幫物件轉換狀態。



3.控制中心

 Hierarchy 上有一個控制中心 GameControllCenter ，裡面的腳本是整體遊戲執行時會需要的腳本。

例如上面提到的 InputManagerCenter 偵測滑鼠點擊，TransparentWindow 把視窗背景變透明 ，MainBoundary 偵測桌面的邊界(現在是static腳本，沒有放在GameControllCenter中)。


4.其他事項

我沒有用Unity的物理引擎Rigidbody，所以物體移動和碰撞是透過position設定和raycast偵測。

我的角色之間的互動系統寫的比較複雜，角色的互動分成執行者和被互動者，執行者的"interactManager"中有可以執行的"互動選項"Array，被互動者也有一個可被執行的"互動選項"Array。
互動選項是透過enum標籤來分類。腳色要執行互動時就會配對雙方的enum標籤來看有沒有能執行的互動。配對成功後，就會去執行雙方互動選項對應的狀態，角色對物體也是同樣。(不確定是否有更好的寫法)



