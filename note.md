專案 "HoloPet"

主要架構
遊戲裡每個的物件(Botan,Cart,Chair...)都有自己的"狀態機"和"狀態"(StateMachine,States)。
物件的"狀態機"腳本上面會有那個物件所有可能進行的"狀態"。
例如.Cart物件的狀態機"CartStateMachine"( 位置在 Assets/Scripts/State&Machine/CartStates)

CartStateMachine 有狀態



