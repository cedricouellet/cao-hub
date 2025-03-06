<script lang="ts">
  import SideBarPage, {
    type SideBarNavGroup,
  } from "@components/core/side-bar-page/side-bar-page.svelte";

  let { children } = $props();

  const navGroups: SideBarNavGroup[] = [
    {
      title: "Receipts",
      items: [
        { text: "Receipt List", href: "/receipt-management/receipts" },
        { text: "Add Receipt", href: "/receipt-management/receipts/create" },
      ],
    },
    {
      title: "Receipt Parameters",
      items: [
        {
          text: "Products",
          href: "/receipt-management/products",
        },
        {
          text: "People",
          href: "/receipt-management/people",
        },
        {
          text: "Taxes",
          href: "/receipt-management/taxes",
        },
      ],
    },
    {
      title: "Stores",
      items: [
        {
          text: "Stores",
          href: "/receipt-management/stores",
        },
        {
          text: "Store Categories",
          href: "/receipt-management/store-categories",
        },
      ],
    },
  ];
</script>

<SideBarPage {navGroups}>
  {@render children()}
</SideBarPage>
