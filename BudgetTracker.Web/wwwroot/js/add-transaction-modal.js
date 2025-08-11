// add-transaction-modal.js
(function(){
  function qs(root, sel){ return (root || document).querySelector(sel); }

  function initModal(root){
    if(!root) return;
    var headerToggle = qs(root, '.morph-header .toggle-inline input[type="checkbox"]');
    var hiddenBind = qs(root, '#isRecurringHidden');
    var recurring = qs(root, '#recurringForm');
    var single = qs(root, '#singleForm');
    if(!headerToggle || !hiddenBind || !recurring || !single) return;

    function updateUI(isOn){
      recurring.style.display = isOn ? '' : 'none';
      single.style.display = isOn ? 'none' : '';
      // sync hidden bound input to notify Blazor state
      if(hiddenBind.checked !== !!isOn){
        hiddenBind.checked = !!isOn;
        hiddenBind.dispatchEvent(new Event('input', { bubbles: true }));
        hiddenBind.dispatchEvent(new Event('change', { bubbles: true }));
      }
    }

    // initialize from current state (either headerToggle or hiddenBind)
    var current = headerToggle.checked || hiddenBind.checked;
    headerToggle.checked = !!current;
    updateUI(!!current);

    headerToggle.addEventListener('change', function(e){
      updateUI(!!e.target.checked);
    });
  }

  // Expose initializer
  window.AddTransactionModal = {
    init: function(){
      var modal = document.getElementById('morphModal');
      if(modal){ initModal(modal); }
    },
    hide: function(){
      var modal = document.getElementById('morphModal');
      if(modal){ modal.style.display = 'none'; }
    },
    show: function(){
      var modal = document.getElementById('morphModal');
      if(modal){ modal.style.display = ''; }
    }
  };
})();
