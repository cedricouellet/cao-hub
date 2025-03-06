<script lang="ts">
  import { toastStore } from '@lib/stores/toast-store';
	import '../app.css';
	import Navbar from '@components/core/navbar/navbar.svelte';
  import Toast from '@components/core/toast/toast.svelte';

	let { children } = $props();
</script>

<header>
	<Navbar />
</header>
<main>
	{@render children()}

	<div class="fixed bottom-0 right-0 flex flex-col gap-2 me-2 mb-2 w-75 z-50">
		{#each $toastStore as toast}
			<Toast message={toast.message} severity={toast.severity} />
		{/each}
	</div>
</main>