$(document).ready(function($){
				$('#dk-accordion-menu').dcAccordion({
					classParent	 : 'dk-accordion-parent',
					classActive	 : 'active',
					classArrow	 : 'accordion-item-icon',
					classCount	 : 'accordion-item-count',
					classExpand	 : 'dk-accordion-current-parent',
					eventType: 'click',
					autoClose: false,
					saveState: false,
					disableLink: false,
					speed: 'slow',
					showCount: false,
					autoExpand: true,
					cookie	: ''
				});
});