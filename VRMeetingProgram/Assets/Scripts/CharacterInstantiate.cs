// ----------------------------------------------------------------------------
// <copyright file="CharacterInstantiation.cs" company="Exit Games GmbH">
// Photon Voice Demo for PUN- Copyright (C) 2016 Exit Games GmbH
// </copyright>
// <summary>
// Class that handles character instantiation when the actor is joined.
// </summary>
// <author>developer@photonengine.com</author>
// ----------------------------------------------------------------------------

namespace ExitGames.Demos.DemoPunVoice
{
    using System.Collections.Generic;
    using ExitGames.Client.Photon;
    using Photon.Realtime;
    using UnityEngine;
    using Photon.Pun;


    public class CharacterInstantiate : MonoBehaviourPunCallbacks
    {
        public enum SpawnSequence { Connection, Random, RoundRobin }

        public Transform SpawnPosition;
        public float PositionOffset = 2.0f;
        public GameObject[] PrefabsToInstantiate;
        public List<Transform> SpawnPoints;

        public bool AutoSpawn = true;
        public bool UseRandomOffset = true;
        public SpawnSequence Sequence = SpawnSequence.Connection;

        public delegate void OnCharacterInstantiated(GameObject character);
        public static event OnCharacterInstantiated CharacterInstantiated;

        [SerializeField] 
        private byte manualInstantiationEventCode = 1;

        protected int lastUsedSpawnPointIndex = -1;

        #pragma warning disable 649
        [SerializeField]
        private bool manualInstantiation;

        [SerializeField] 
        private bool differentPrefabs;

        [SerializeField] private string localPrefabSuffix;
        [SerializeField] private string remotePrefabSuffix;
        #pragma warning restore 649

        public override void OnJoinedRoom()
        {
            if (this.PrefabsToInstantiate != null)
            {
                int index = 0; // 후에 자신이 커스텀한 데이터를 불러올 수 있어야 함.
                Vector3 spawnPos = new Vector3(0, 0, 0);
                //Quaternion spawnRotation;
                Camera.main.transform.position += spawnPos;
                GameObject o = this.PrefabsToInstantiate[index];
                o = PhotonNetwork.Instantiate(o.name, spawnPos, Quaternion.identity);
                if (CharacterInstantiated != null)
                {
                    CharacterInstantiated(o);
                }
            }
        }
    }

}