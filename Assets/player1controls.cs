// GENERATED AUTOMATICALLY FROM 'Assets/player1controls.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class Player1controls : InputActionAssetReference
{
    public Player1controls()
    {
    }
    public Player1controls(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // player1
        m_player1 = asset.GetActionMap("player1");
        m_player1_South = m_player1.GetAction("South");
        m_player1_East = m_player1.GetAction("East");
        m_player1_North = m_player1.GetAction("North");
        m_player1_West = m_player1.GetAction("West");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_player1 = null;
        m_player1_South = null;
        m_player1_East = null;
        m_player1_North = null;
        m_player1_West = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // player1
    private InputActionMap m_player1;
    private InputAction m_player1_South;
    private InputAction m_player1_East;
    private InputAction m_player1_North;
    private InputAction m_player1_West;
    public struct Player1Actions
    {
        private Player1controls m_Wrapper;
        public Player1Actions(Player1controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @South { get { return m_Wrapper.m_player1_South; } }
        public InputAction @East { get { return m_Wrapper.m_player1_East; } }
        public InputAction @North { get { return m_Wrapper.m_player1_North; } }
        public InputAction @West { get { return m_Wrapper.m_player1_West; } }
        public InputActionMap Get() { return m_Wrapper.m_player1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
    }
    public Player1Actions @player1
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new Player1Actions(this);
        }
    }
}
