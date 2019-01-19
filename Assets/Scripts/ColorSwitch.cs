using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSwitch : AudioSyncer {

    Renderer rend;

    public Color[] beatColors;
    public Color restColor;

    private int m_randomIndx;
    private Color m_img;

    void Start()
    {
        rend = GetComponent<Renderer>();

        //Get the color of the material
        m_img = rend.material.GetColor("_Color");
    }

    void Update()
    {
        OnUpdate();
    }

    private IEnumerator MoveToColor(Color _target)
	{
        //Get the current value of the material
        Color _curr = m_img;
		Color _initial = _curr;
		float _timer = 0;
		
		while (_curr != _target)
		{
            //Lerp (mix) the initial value with the target beat value, depending on the timeToBeat value
            _curr = Color.Lerp(_initial, _target, _timer / timeToBeat);
			_timer += Time.deltaTime;

            //Set the new values to the variable
            m_img = _curr;

			yield return null;
		}
		m_isBeat = false;
	}

	private Color RandomColor()
	{
        //Choose one of the random colors that the user will choose
		if (beatColors == null || beatColors.Length == 0) return Color.white;
		m_randomIndx = Random.Range(0, beatColors.Length);
		return beatColors[m_randomIndx];
	}

	public override void OnUpdate()
	{
		base.OnUpdate();

		if (m_isBeat) return;

        //If no beat is being registrered, Lerp back to the initial value
        rend.material.SetColor("_Color", Color.Lerp(m_img, restColor, restSmoothTime * Time.deltaTime)); 
	}

	public override void OnBeat()
	{
		base.OnBeat();

		Color _c = RandomColor();

		StopCoroutine("MoveToColor");
		StartCoroutine("MoveToColor", _c);
	}
}