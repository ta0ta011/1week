using UnityEngine;
using VContainer.Unity;

//　初期化：準備する。テキスト・画像とかの読み込みや状態を戻すとか
//　コンストラクタ：インスタンス化・ピュアC#でクラスを使えるようにする。箱を生成して、その箱の中身を使えるようにする方法

public interface IStateContext 
{
	//UIや演出への橋渡し
	public void ChangeState<T>(ScopeType scopeType) where T : IState;

	//画面切り替え										   
	//State切り替え
	//フェーズ切り替えでCreateScopeを使う										   
	//LifetimeScope：「DIコンテナ + 寿命管理 + EntryPoint起動装置」

	//スコープを作る
	LifetimeScope CreateScope(ScopeType scopeType);
}
 