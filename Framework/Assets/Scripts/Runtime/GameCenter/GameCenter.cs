using BuffModule.Runtime;
using Module.FrameBase;
using Module.GameCore;
using Runtime.AbilityModule.Runtime.Framework;
using Runtime.Framework.FrameUtility;
using UnityEngine;

public class GameCenter : MonoBehaviour
{
    private LogicScene _clientScene = null;//所有CoreEntity的Domain
    private GameSingletonCenter _singletonCenter = null;//GameCenter中心，所有单例管理节点

    private BattleEntity _battleEntity;

    private void Awake()
    {
        _singletonCenter = new GameSingletonCenter();
        _singletonCenter.AddSingleton<CoreEventSystem>();
        _singletonCenter.AddSingleton<IdGenerator>();
        _singletonCenter.AddSingleton<LogicEntityCollector>();
        _singletonCenter.NotifyAllSingletonCreateComplete();
        
        _clientScene = SceneEntityFactory.CreateLogicScene(0, SceneType.Client, "ClientScene");
        _clientScene.AddComponent<CameraModuleEntity>();
        _clientScene.AddComponent<BuffModuleEntity>();
        _battleEntity = _clientScene.AddComponent<BattleEntity>();
    }

    void Start()
    {
        _battleEntity?.OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        _singletonCenter.Update();
    }
}
