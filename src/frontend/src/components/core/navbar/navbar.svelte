<script>
  import { page } from "$app/state";

  const linkClass = "mx-4 my-1";
  const activeLinkClass = "text-light-disabled";
  const inactiveLinkClass = "hover:text-light-secondary";
</script>

<div class="relative mb-16 z-1">
  <div class="fixed top-0 right-0 left-0">
    <div
    class=" flex flex-col md:flex-row gap-1 md:gap-6 bg-neutral-dark-secondary text-light shadow-md p-4 items-center"
  >
    <img src="/img/logo.svg" alt="logo" class="h-[35px]" />
    <a
      href="/"
      class="{linkClass} {page.route.id === '/'
        ? activeLinkClass
        : inactiveLinkClass}">Home</a
    >
    <a
      href="/receipt-management"
      class="{linkClass} {page.route.id?.startsWith('/receipt-management')
        ? activeLinkClass
        : inactiveLinkClass}">Receipt Management</a
    >
  </div>
  </div>
  
</div>

