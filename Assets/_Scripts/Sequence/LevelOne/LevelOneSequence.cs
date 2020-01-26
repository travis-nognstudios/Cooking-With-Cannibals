﻿using UnityEngine;
using System.Collections;

namespace Sequence
{
    public class LevelOneSequence : SequenceManager
    {
        protected override void CreateNodes()
        {
            SequenceNode bruceIntro = GetComponent<PlayHeroIntro>();
            SequenceNode awningOpen = GetComponent<AwningOpenSequence>();
            SequenceNode gameplayLoop = GetComponent<PlayGameLoop>();
            SequenceNode bruceOutro = GetComponent<PlayHeroOutro>();

            nodes.Add(bruceIntro);
            nodes.Add(awningOpen);
            nodes.Add(gameplayLoop);
            nodes.Add(bruceOutro);
        }

    }
}