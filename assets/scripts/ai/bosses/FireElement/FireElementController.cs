using UnityEngine;
using System.Collections;

public class FireElementController : MonoBehaviour {
    public int Difficulty;

    //Animation
    public AnimationCurve IntroAnimationCurve;
    public float IntroAnimationTime;
    public Vector3 Endposition;
    private float AnimationPos = 0f;
    private float step = 0f;
    private bool IntroAnimationSwitch = false;

    //Spells
    public GameObject castPoint;
    public GameObject Spell1; //FireBall spell For FireNova
    public float[] Degreasplit; //The Degrea between the spells
    public float[] Cooldown;
    private float NextSpell;

    //Info
    private int Stage;


    void InitFight()
    {
        Difficulty = GameState.instance.Difficulty;
        Stage = 0;
        IntroAnimationSwitch = false;
        NextSpell = Time.time + Cooldown [Difficulty];
    }


    void Update()
    {
        //Intro Animation
        if(IntroAnimationSwitch)
            IntroAnimation();

        //CastSpell
        if (NextSpell < Time.time)
            CastSpell1();
    }

    void CastSpell1()
    {
        int AmountOfSpells = Mathf.RoundToInt(360f / Degreasplit [Difficulty]);
        Transform CastPoint = castPoint.transform;
//        CastPoint.transform.Translate(new Vector3(0, 2, 0));
        for (int i = 0; i < AmountOfSpells; i++)
        {
            Instantiate(Spell1, CastPoint.position, CastPoint.rotation);
            CastPoint.Rotate(new Vector3(0, Degreasplit[Difficulty], 0));
        }
        NextSpell = Time.time + Cooldown [Difficulty];
    }

    //Animation controls
	public void InitAnimation()
    {
        step = 0f;
        AnimationPos = 0f;
        IntroAnimationSwitch = true;
    }

    public void IntroAnimation()
    {
        transform.position = Vector3.MoveTowards(transform.position, Endposition, step);
        AnimationPos += Time.deltaTime / IntroAnimationTime;
        step = IntroAnimationCurve.Evaluate(AnimationPos);
        if(Vector3.Distance(transform.position, Endposition) < 1f)
            InitFight();
    }

}
