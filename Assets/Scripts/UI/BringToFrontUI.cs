using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;


/// <summary>This class bring a UI to front, when on click.</summary>
[Serializable]
public partial class BringToFrontUI : MonoBehaviour, IPointerDownHandler
{
	[NonSerialized] public RectTransform rect;
	[SerializeField] public Color ActiveWindowColor;
	[SerializeField] public Color InActiveWindowColor;
	[SerializeField] public Image ThisWindowRect;
	[SerializeField] public bool FrontOnStartup = false;
	private BringToFrontUIHelper[] children;

	protected virtual void Awake()
	{
		rect = GetComponent<RectTransform> ();
		children = ThisWindowRect.GetComponentsInChildren<BringToFrontUIHelper>();

		if(FrontOnStartup == true)
			Invoke("BringToFront",0.01f);
	}

	public virtual void OnPointerDown(PointerEventData e)
	{
		BringToFront();
	}
	void OnEnable()
	{
		BringToFront();
	}
	void Update()
	{
		//set window with inactive caption if not last in the list
		if(ThisWindowRect != null && ThisWindowRect.color == ActiveWindowColor && rect.GetSiblingIndex() < (rect.parent.childCount - 1))
		{
			ThisWindowRect.color = InActiveWindowColor;
			foreach(BringToFrontUIHelper child in children)
			{
				child.SetColor(InActiveWindowColor);
			}
		}
	}

	public void BringToFront()
	{
		rect.SetAsLastSibling ();
		if(ThisWindowRect != null)
		{
			ThisWindowRect.color = ActiveWindowColor;
			foreach(BringToFrontUIHelper child in children)
			{
				child.SetColor(ActiveWindowColor);
			}
		}
	}
}