using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeskRoom : Room {


	SinnerScript onDeskSinner;
	public GameObject GoFirstRoomGUIPrefab;

	private int capacity = 8;
	private List<SinnerScript> waitingSinnersList;

	//Positions to guide the sinner
	[SerializeField] private Transform entryTransform;
	[SerializeField] private Transform firstOnLineTranform;
	[SerializeField] private Transform lastOnLineTranform;
	[SerializeField] private Transform daskWaitTransform;

	private List<Room> firstRoomsList = new List<Room>();

	public override void Awake(){
		base.Awake ();
		waitingSinnersList = new List<SinnerScript> ();
	}

	public void Start(){
		InvokeRepeating ("TryToSendNextToDesk", 1f, 1f);
	}
	
	void Update () {
	}

	public override bool HasNextRoom ()
	{
		return false;
	}

	public override bool CanSinnerArrive ()
	{
		return this.waitingSinnersList.Count < capacity;
	}

	public override bool CanSetNextRoom ()
	{
		return false;
	}

	public override void OnSinnerArive (SinnerScript sinner)
	{
		sinner.SetBig (true);
		waitingSinnersList.Add (sinner);

		Vector3 startPos = entryTransform.position;//new Vector3 (this.transform.position.x - (roomWidth/2 + sinnerWidth/2), this.transform.position.y, 0);
		sinner.transform.position = startPos;
		RearangeSinnersPosition ();

	}


	//When The sinner Arrive at DEsk, A GUI show to player choose where to go
	public void ShowGUIOfFirstRooms(SinnerScript si){
		GameObject guiHolder = new GameObject ("GUI HOLDER");
		guiHolder.transform.SetParent (this.transform);
		foreach(Room r in firstRoomsList){
			GameObject gUICanvasMoveToFirstRoom = Instantiate(GoFirstRoomGUIPrefab, r.transform.position, Quaternion.identity) as GameObject;
			gUICanvasMoveToFirstRoom.transform.SetParent(guiHolder.transform);
			Room capturedRoom = r;
			gUICanvasMoveToFirstRoom.transform.Find("MoveButton").gameObject.GetComponent<Button>().onClick.AddListener(() => {
				if(capturedRoom.CanSinnerArrive()){
					this.NextRoom = capturedRoom;
					capturedRoom.ReserveSinnerPlace();
					onDeskSinner.MoveToTarget (this.transform.position + new Vector3 (roomWidth / 2 + sinnerWidth / 2, 0, 0), WalkOutsideCallBack);
					Destroy(guiHolder);
					Invoke("FreeDeskForNextSinner", 0.8F);
				}
			});
		}
	}

	public void AddToFirstRoomsList(Room room){
		firstRoomsList.Add (room);
		Transform gh = transform.Find("GUI HOLDER");
		if (gh != null) {
			Destroy(gh.gameObject);
			ShowGUIOfFirstRooms(onDeskSinner);
		}

	}

	public void RemoveFromFirstRoomsList(Room room){
		firstRoomsList.Remove (room);
		Transform gh = transform.Find("GUI HOLDER");
		if (gh != null) {
			Destroy(gh.gameObject);
			ShowGUIOfFirstRooms(onDeskSinner);
		}
	}

	public List<Room> GetFirstRoomsList(){
		return firstRoomsList;
	}

	private void FreeDeskForNextSinner(){
		this.onDeskSinner = null;
		//Gritar, PROXIMO!!!
	}

	public override void WalkOutsideCallBack (SinnerScript sinner)
	{
		sinner.SetBig (false);
		base.WalkOutsideCallBack (sinner);
		this.NextRoom = null;

	}

	public void TryToSendNextToDesk(){
		if (waitingSinnersList.Count > 0 && onDeskSinner == null) {
			onDeskSinner = waitingSinnersList[0];
			waitingSinnersList.Remove(onDeskSinner);

			onDeskSinner.MoveToTarget(daskWaitTransform.position, ShowGUIOfFirstRooms);

			RearangeSinnersPosition();
		}
	}

	private void RearangeSinnersPosition(){

		foreach (SinnerScript si in waitingSinnersList) {
			Vector3 target = firstOnLineTranform.position - waitingSinnersList.IndexOf(si)*(firstOnLineTranform.position - lastOnLineTranform.position)/(capacity-1);
			si.MoveToTarget(target, null);
		}
	}

}