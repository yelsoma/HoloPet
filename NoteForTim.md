�M�� "HoloPet" �D�n�[�c

1.�C������

 Hierarchy�W������(Botan,Cart,Chair...)�����U�۪�"StateMachine�M"States"�}���C
�C�Ӫ���W��"���A��"(StateMachine)�M"���A"(States)�}���A���O�~��"StateMachineBase"�M"StateBase"�}���C(�}����m�b Assets/Scripts/State&Machine/FiniteStateMachineBase)

�Ҧp"Cart"����W��:
"CartStateMachine" (Cart�����A���}��)        
"CartState_Fall"   (Cart���������A�}��)
"CartState_Idle"   (Cart���o�b���A�}��)
"CartState_Grab"   (Cart���Q������A�}��)
(�}����m���b Assets/Scripts/State&Machine/CartStates )

 ����û��|���@�ӥ��b���檺���A�A����ͦ��ɫK�|�z�L���A���i�J�Ӫ��󪺲Ĥ@�Ӫ��A�C�æb�F���@�w������ഫ��U�@�Ӫ��A�C
����"���A��"�}���W���|�i��i��"���A"�}����referance�C
 
�Ҧp"Cart"�����A�� "CartStateMachine"���Ĥ@�Ӫ��A�O "CartState_Spawn"(�ͦ����A)�C 
�ͦ����A�|�]�w�@�Ǫ�l�ȡA�M�᪽���i�J"CartState_Idle"(�o�b���A)�A�i�J�o�b���A��|����i��a���˴� CheckIsBotBoundery()�A�p�G���b�a��W�h�i�J"CartState_Fall"�������A�A������F�a��W�S�|�^��o�b���A�C



2.���A�B���k

�Ҧ�"���A"�}�������X���~�� StateBase����k
public override void Enter(){};  �i�J���A�ɭn����@������k
public override void StateUpdate(){};  ���A���n������檺��k
public override void Exit(){};   ���}���A�ɭn����@������k

�o�Ǥ�k�|�b"���A��"�~��"StateMachineBase"����
private void Start(){};
private void Update();
public void ChangeState(StateBase newState){};
�̭��B��

���ܪ��A�����󦳴X�إi��
 �@�A���A���槹��(ex: State_Spawn �]�w����l�ȫ�A�N���������i�JState_Idle )
 �G�A���A���椤�F�����(ex: State_Fall �|���_�����M GroundCheck�AGroundCheck���\��N�i�J State_Idle )
 �T�A����~���������ܪ��A(ex: �A���N���A�U�A����Q�ƹ��I���즲�A���|�i�J State_Grab �Q�즲���A)
�ĤT�ا��ܪ��A���覡�N�|�ݭn�z�L Managers �ӱ���C

�Ҧp Cart���� ���ƹ��I���O�z�L "CartInputManager" ����C (�}����m�b Assets/Scripts/ObjectManagers/ObjectManagers/CartManager)
�I���ƹ��ɡAHierarchy �W GameControllCenter ���� InputManagerCenter �|���Q�I������ iClickable interface ("CartInputManager") �M�᪫�� iClickable �N�|�����������ഫ���A�C



3.�����

 Hierarchy �W���@�ӱ���� GameControllCenter �A�̭����}���O����C������ɷ|�ݭn���}���C

�Ҧp�W�����쪺 InputManagerCenter �����ƹ��I���ATransparentWindow ������I���ܳz�� �AMainBoundary �����ୱ�����(�{�b�Ostatic�}���A�S����bGameControllCenter��)�C


4.��L�ƶ�

�ڨS����Unity�����z����Rigidbody�A�ҥH���鲾�ʩM�I���O�z�Lposition�]�w�Mraycast�����C

�ڪ����⤧�������ʨt�μg����������A���⪺���ʤ�������̩M�Q���ʪ̡A����̪�"interactManager"�����i�H���檺"���ʿﶵ"Array�A�Q���ʪ̤]���@�ӥi�Q���檺"���ʿﶵ"Array�C
���ʿﶵ�O�z�Lenum���ҨӤ����C�}��n���椬�ʮɴN�|�t�����誺enum���ҨӬݦ��S������檺���ʡC�t�令�\��A�N�|�h�������褬�ʿﶵ���������A�A����磌��]�O�P�ˡC(���T�w�O�_����n���g�k)



