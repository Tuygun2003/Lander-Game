using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance { get; private set; }

    public event EventHandler OnUpForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnBeforeForce;
    public event EventHandler CoinSystem;
    public event EventHandler<OnLandedEventArgs> OnLanded;
    public class OnLandedEventArgs
    {
        public LandingType LandingType;
        public int score;
        public float dotVector;
        public float landingSpeed;
        public float scoreMultiplier;

    }

    public enum LandingType
    {
        Success,
        WrongLandingArea,
        TooSteepAngle,
        ToFastLanding,
    }




    private Rigidbody2D playerRigidbody2D;
    bool yukarýgidis=false;
    bool sagadönüs=false;
    bool soladönüs=false;
    float fuelAmount;
    float fuelAmountMax = 10f;
    private void Awake()
    {
        Instance = this;
        fuelAmount = fuelAmountMax;
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        OnBeforeForce?.Invoke(this, EventArgs.Empty);

        if(fuelAmount <= 0f)
        {
            //Nofuel
            return;
        }



        if(Keyboard.current.upArrowKey.isPressed|| Keyboard.current.rightArrowKey.isPressed|| Keyboard.current.leftArrowKey.isPressed)
        {
            ConsumeFuel();
        }




        if (yukarýgidis)
        {
            float forwardforce = 700f;
            playerRigidbody2D.AddForce(forwardforce*transform.up*Time.deltaTime);
            
            OnUpForce?.Invoke(this,EventArgs.Empty);

        }
        if(sagadönüs) 
        {
            float rightforce = -100f;
            playerRigidbody2D.AddTorque(rightforce*Time.deltaTime);
            
            OnRightForce?.Invoke(this, EventArgs.Empty);
        }
        if(soladönüs)
        {
            float leftforce = 100f;
            playerRigidbody2D.AddTorque(leftforce * Time.deltaTime);
            
            OnLeftForce?.Invoke(this, EventArgs.Empty);
        }
       
        


    }
    void Update()
    {
        yukarýgidis = Keyboard.current.upArrowKey.isPressed;
        sagadönüs= Keyboard.current.rightArrowKey.isPressed;
        soladönüs=Keyboard.current.leftArrowKey.isPressed;
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out LandingPad landingpad))
        {
            print("Crashed the terrain");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                LandingType = LandingType.WrongLandingArea,
                dotVector = 0f,
                landingSpeed = 0f,
                scoreMultiplier = 0f,
                score = 0
            });
            return;
        }



        float softRelativeVelocityMagnitude = 4f;
        float relativeVelocityMagnitude = collision.relativeVelocity.magnitude;
        if (relativeVelocityMagnitude > softRelativeVelocityMagnitude)
        {
            print("kötü iniþ");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                LandingType = LandingType.ToFastLanding,
                dotVector = 0f,
                landingSpeed = relativeVelocityMagnitude,
                scoreMultiplier = 0f,
                score = 0
            });
            return;
        }

        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minDotVector = 0.90f;

        if (dotVector < minDotVector)
        {
            print("kötü iniþ 2");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                LandingType = LandingType.TooSteepAngle,
                dotVector = dotVector,
                landingSpeed = relativeVelocityMagnitude,
                scoreMultiplier =0f,
                score = 0
            });
            return;
        }

        print("iyi iniþ");


    float maxScoreAmountLandingAngle = 100f;
    float scoreDotVectorMultiplier = 10f;
    float landingAngleScore = maxScoreAmountLandingAngle - Mathf.Abs(dotVector - 1f) * scoreDotVectorMultiplier * maxScoreAmountLandingAngle;


    float maxScoreAmountLandingSpeed = 100f;
    float landingSpeedScore = (softRelativeVelocityMagnitude - relativeVelocityMagnitude) * maxScoreAmountLandingAngle;

        Debug.Log("LangingAngleScore: "+ landingAngleScore);
        Debug.Log("LandingSpeedScore: " + landingSpeedScore);


        int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * landingpad.GetMultiplierScore());

        OnLanded?.Invoke(this, new OnLandedEventArgs {
            LandingType=LandingType.Success,
            dotVector=dotVector,
            landingSpeed=relativeVelocityMagnitude,
            scoreMultiplier=landingpad.GetMultiplierScore(),
            score = score,
        });


    }

    private void ConsumeFuel()
    {
        float consumeFuelAmount = 1f;
        fuelAmount -= consumeFuelAmount*Time.deltaTime;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Fuel fuel))
        {
            float AmountofAddFuel=10f;
            fuelAmount += AmountofAddFuel;
            if (fuelAmount > fuelAmountMax)
            {
                fuelAmount=fuelAmountMax;
            }
            fuel.DestroySelf();
        }
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            coin.DestroySelf();
            CoinSystem?.Invoke(this, EventArgs.Empty);
        }
    }


    public float GetFuelAmountNormalized()
    {
        return fuelAmount / fuelAmountMax;

    }
    public float GetSpeedX()
    {
        return playerRigidbody2D.linearVelocityX;

    }
    public float GetSpeedY()
    {

        return playerRigidbody2D.linearVelocityY;
    }



}
