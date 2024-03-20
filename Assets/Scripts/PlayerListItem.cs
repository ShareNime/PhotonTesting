using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _playerNametext;
    [SerializeField] TMP_Text _playerDeadText;
    Player player;
    public void SetUp(Player _player)
    {
        player = _player;
        _playerNametext.text = _player.NickName;
        // _playerDeadText.text = "Deaths: " + _player.CustomProperties["DeadCounter"].ToString();
        UpdateStats(_player);
        Debug.Log("PlayerListItem setup: " + _player.NickName);
    }
    void UpdateStats(Player _player)
    {

        if (_player.CustomProperties.TryGetValue("DeadCounter", out object deaths))
        {
            _playerDeadText.text = "Deaths: " + deaths.ToString();
        }
        else
        {
            _playerDeadText.text = "IDK";
            Debug.Log("LINE 24" + _player.CustomProperties["DeadCounter"]);
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (targetPlayer == player)
        {
            if (changedProps.ContainsKey("DeadCounter"))
            {
                UpdateStats(targetPlayer);
                Debug.Log("Ada perubahan value pada deadcounter");
            }
        }
    }
}
