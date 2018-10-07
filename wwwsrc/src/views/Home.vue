<template>
  <div class="home">
    <navigation-bar />
    <keeps />
  </div>
</template>

<script>
import NavigationBar from "@/components/NavigationBar";
import Keeps from "@/components/Keeps";
  export default {
    name: "home",
    mounted() {
      //blocks users not logged in
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      }
      this.$store.dispatch("getKeeps", 0) //will have to increment argument. maybe in store is better idea anyway
    },
    components: {
      NavigationBar,
      Keeps
    }
  };
</script>