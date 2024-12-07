# 独自のメニューを追加する方法

独自のメニューを追加する方法を解説します。

## 追加方法

NOA Debuggerを組み込んだアプリケーション内で独自のメニューを利用するには、1つのメニューに対して以下の対応が必要です。

- uGUIを用いたPrefabの作成
- `NoaCustomMenuBase`を継承したC#クラスの実装
- NOA Debugger EditorからC#クラスの登録

NOA Debuggerが提供する機能を利用する場合は、必ず`NOA_DEBUGGER`のシンボル定義を利用してください。<br>
継承したクラスには以下のプロパティをオーバーライドして実装する必要があります。

| プロパティ                          |                                 |
|--------------------------------|---------------------------------|
| string ViewPrefabPath { get; } | ツール内に表示する画面のPrefabパスを返り値に指定します。 |
| string MenuName { get; }       | メニューに表示する名前を返り値に指定します。          |

また、以下のメソッドは必要に応じてオーバーライドできます。

| メソッド                         |                                                                                          |
|------------------------------|------------------------------------------------------------------------------------------|
| void OnInitialize()          | 初期化時に行いたい処理を記載します。<br>NOA Debuggerの初期化時に実行されます。<br>APIのNoaDebug.Initialize()を実行した際も含みます。 |
| void OnShow(GameObject view) | ツール内部表示時の処理を記載します。<br>引数：生成されたGameObjectが取得できます。                                         |
| void OnHide()                | ツール内部非表示時の処理を記載します。<br>NOA Debuggerを閉じた時・メニューの切り替え時に実行されます。                              |
| void OnDispose()             | ツールの破棄時に行いたい処理を記載します。<br>NOA Debugger破棄時に実行されます。<br>APIのNoaDebug.Destroy()を実行した際も含みます。   |

継承したクラスをNOA Debugger Editorから登録する方法は、[ツールの設定方法](./Settings.md)を参照してください。

ツールに表示する画面のPrefabは`Assets/NoaDebuggerSettings/Resources/Custom`フォルダに配置してください。<br>
Prefabに配置するTransformは`Rect Transform`で作成してください。

![Inspector画面](../img/custom-menu/inspector.png)

NOA DebuggerのCanvasはRender ModeがOverlayで設定しているため、3DオブジェクトをRootPrefabの子要素に配置した場合はUIの背面に表示されます。

作成したPrefabは、NOA DebuggerのPrefabと同様にコンパイルに含めないようにすることができます。<br>
詳しい内容は、[ツールを取り除いてコンパイルする方法](./ExcludingFromCompile.md)を参照してください。

#### サンプルコード

```csharp
using UnityEngine;
#if NOA_DEBUGGER
using NoaDebugger;

public class SampleMenu : NoaCustomMenuBase
{
    protected override string ViewPrefabPath { get => "Custom/SampleView"; }

    protected override string MenuName { get => "sample"; }

    private GameObject View { get; set; }

    protected override void OnInitialize()
    {
        //何らかの処理
    }

    protected override void OnShow(GameObject view)
    {
        // ViewのGameObjectを取得
        View = view;

        //何らかの処理
    }

    protected override void OnHide()
    {
        //何らかの処理


        // 必要に応じてViewのGameObjectを破棄
        // ViewのGameObjectを破棄しない場合はOnShow時に生成済みのGameObjectを再利用します
        Object.Destroy(View);
    }

    protected override void OnDispose()
    {
        //何らかの処理
    }
}
#endif
```

## 表示方法

NOA Debuggerツールを起動後、メニュー欄の下部にある[Custom Menu]を押下します。

![独自メニュー切替画面](../img/custom-menu/custom-menu-change.png)

押下で追加したメニューのみの画面に切り替わり、NOA Debugger提供メニューと同様にメニューを選択して詳細を表示できます。<br>
NOA Debugger提供メニューに戻る際はメニュー欄の下部にある[Default Menu]を押下します。
