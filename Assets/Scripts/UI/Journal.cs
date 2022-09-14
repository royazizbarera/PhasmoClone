using Infrastructure;
using Managers.Services;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Journal : MonoBehaviour
{
    [SerializeField]
    private GameObject _book;

    [SerializeField]
    private List<GameObject> _pages;
    [SerializeField]
    private List<GhostDataSO> _ghostsData;

    [SerializeField]
    private GameObject _ghostTypesPage;
    [SerializeField]
    private GameObject _ghostDescription;

    [SerializeField]
    private TextMeshProUGUI _leftPageNumTXT;
    [SerializeField]
    private TextMeshProUGUI _rightPageNumTXT;

    [SerializeField]
    private TextMeshProUGUI _ghostNameTXT;
    [SerializeField]
    private TextMeshProUGUI _ghostDescriptionTXT;
    [SerializeField]
    private TextMeshProUGUI _ghostEvidencesTXT;

    private InputSystem _inputSystem;
    private int _totalNormalPages;
    private int _totalPagesInBook;
    private int _currentPage = 1;

    private void Awake()
    {
        _totalNormalPages = _pages.Count;
        _totalPagesInBook = _pages.Count + _ghostsData.Count * 2;

        _inputSystem = AllServices.Container.Single<InputSystem>();
        _inputSystem.JournalOpenAction += ActivateJornal;
        _currentPage = 1;
        ChangeCurrentPage(_currentPage);
    }

    private void OnDestroy()
    {
        _inputSystem.JournalOpenAction -= ActivateJornal;
    }
    public void ChangeCurrentPage(int page)
    {
        if (_currentPage == page) return;

        if (_currentPage > _totalNormalPages && page > _totalNormalPages)
        {
            _currentPage = page;
            SetUpGhostInfo(page);
        }
        else
        {
            ActivatePage(_currentPage, false);
            _currentPage = page;
            ActivatePage(_currentPage, true);
        }
        _leftPageNumTXT.text = _currentPage.ToString();
        _rightPageNumTXT.text = (_currentPage + 1).ToString();
    }

    public void PreviousPage()
    {
        if (_currentPage - 1 <= 1) return;
        ChangeCurrentPage(_currentPage - 2);
    }

    public void NextPage()
    {
        if (_currentPage + 1 >= _totalPagesInBook) return;
        ChangeCurrentPage(_currentPage + 2);
    }

    private void ActivateJornal()
    {
        if (_book.activeInHierarchy) _book.SetActive(false);
        else _book.SetActive(true);
    }

    private void ActivatePage(int pageNum, bool enable = true)
    {
        if(pageNum > _totalNormalPages)
        {
            SetUpGhostInfo(pageNum);
            _ghostTypesPage.SetActive(enable);
            _ghostDescription.SetActive(enable);
        }
        else
        {
            _pages[pageNum - 1].SetActive(enable);
            _pages[pageNum].SetActive(enable);
        }
    }

    private void SetUpGhostInfo(int currPage)
    {   
        int ghostNum = ((currPage - _totalNormalPages) + 1) / 2;
        ghostNum--;
        _ghostNameTXT.text = _ghostsData[ghostNum].name;
        _ghostDescriptionTXT.text = _ghostsData[ghostNum].GhostDescription;
        _ghostEvidencesTXT.text = "";
        for(int i = 0; i< _ghostsData[ghostNum].GhostEvidences.Count; i++)
        {
            _ghostEvidencesTXT.text = _ghostEvidencesTXT.text + _ghostsData[ghostNum].GhostEvidences[i].ToString() + "\n";
        }
    }

}
