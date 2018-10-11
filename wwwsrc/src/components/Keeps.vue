<template>
    <v-card @mouseenter="showActions(keep.id)" @mouseleave="hideActions(keep.id)">
        <v-footer v-if="fromVault" absolute card color="transparent">
            <v-spacer></v-spacer>
            <v-btn @click="removeKeepFromVault(keep.id)" fab flat small>
                <v-icon>far fa-times-circle</v-icon>
            </v-btn>
        </v-footer>
        <v-img v-if="keep.img.length > 0" :src="keep.img" class="white--text">
            <v-layout column fill-height>
                <v-spacer></v-spacer>
                <v-card-title class="img-title">{{keep.name}}</v-card-title>
            </v-layout>
        </v-img>
        <v-card-title v-if="keep.img.length < 1">{{keep.name}}</v-card-title>
        <v-card-text>{{keep.description}}</v-card-text>
        <v-card-actions :id="fromProfile ? 'P'+keep.id : fromVault ? 'VK'+keep.id : keep.id" class="buttons"> 
            <view-keep :keep="keep"/>
            <v-btn flat fab small color="amber darken-4">keep</v-btn> 
            <v-speed-dial v-model="shareExpand" direction="right" transition="slide-y-reverse-transition">
                <v-btn slot="activator" v-model="shareExpand" color="amber darken-4" fab flat small>
                    <v-icon>fa-share</v-icon>
                    <v-icon>close</v-icon>
                </v-btn>
                <v-btn class="ml-2" fab small color="blue">
                    <v-icon>fab fa-facebook-f</v-icon>
                </v-btn>
                <v-btn v-if="keep.img.length > 0" fab small color="#e62774">
                    <v-icon>fab fa-instagram</v-icon>
                </v-btn>
                <v-btn v-else fab small color="black" class="white--text">
                    <v-icon>fab fa-twitter</v-icon>
                </v-btn>
            </v-speed-dial>
            <v-spacer></v-spacer>
            <v-btn v-if="keep.userId == user.id" @click="deleteKeep(keep.id, keep.userId)" fab flat small color="amber darken-4">
                    <v-icon>fas fa-trash-alt</v-icon>
                </v-btn>
        </v-card-actions>
    </v-card>     
</template>
<script>
import ViewKeep from "@/components/ViewKeep";
export default {
  name: "keeps",
  data() {
    return {
      shareExpand: false
    };
  },
  props: ["keep", "fromProfile", "fromVault", "vaultId"],
  components: {
    ViewKeep
  },
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  methods: {
    showActions(id) {
      if (!this.fromProfile) {
        $(`#${id}`).css({ display: "flex" });
        return;
      }
      $(`#P${id}`).css({ display: "flex" });
    },
    hideActions(id) {
      if (!this.fromProfile) {
        $(`#${id}`).css({ display: "none" });
      }
      $(`#P${id}`).css({ display: "none" });
      this.shareExpand = false;
    },
    deleteKeep(keepId, authorId) {
      let k = {
        id: keepId,
        userId: authorId,
        sendingUserId: this.user.id
      };
      this.$store.dispatch("deleteKeep", k);
    },
    removeKeepFromVault(keepId) {
      let vk = {
        keepId,
        vaultId: this.vaultId
      };
      this.$store.dispatch("removeKeepFromVault", vk);
    }
  }
};
</script>
<style scoped>
.img-title {
  text-shadow: 0 0 5px black;
  font-size: 120%;
}
.buttons {
  display: none;
}
@media (hover: none) {
  .buttons {
    display: flex;
  }
}
</style>
